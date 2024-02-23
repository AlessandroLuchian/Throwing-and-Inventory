using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class EnemyScript : MonoBehaviour
{
     enum States
    {
     Wander,
     Idle,
     Chase,
     Attack
    }

    private States state = States.Wander;
    private Vector3 startPosition;  //Give it a center for it's random wandering.
    [SerializeField] private float Speed; // sets how fast ghosty moves
    [SerializeField] private float Range; // sets how far ghosty goes on random movement
    [SerializeField] private List<GameObject> enemyWaipoints; // waypoint list(made it gameobject so it's easier to set up)
    private NavMeshAgent agent; // no touchy
    private int waypointIndex = 0;
    private Vector3 currentWaypoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
        startPosition = this.transform.position;
    }

    // private void Wander(Vector3 displacement)
    // {
    //     Vector3 destination = startPosition + displacement; 
    //     NewDestination(destination);
    // }

    public void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         agent.speed = Speed;
         startPosition = this.transform.position;
         Vector3 currentWaypoint = this.enemyWaipoints[0].transform.position;
    }

    private Coroutine _currentRoutine;

    private bool IsArrived()
    {
        return Vector3.Distance(this.transform.position, agent.destination) < 1.0f;
    }

    public void Move(Vector3 position)
    {
        if(_currentRoutine != null) return;

        agent.SetDestination(position);

        _currentRoutine = StartCoroutine(WaitUntilArrivedPosition(position));
    }

    private IEnumerator WaitUntilArrivedPosition(Vector3 position)
    {
        yield return new WaitUntil(IsArrived);
        _currentRoutine = null;
        yield return new WaitForSeconds(1f);
      
    }

    IEnumerator chaseDelay()
    {
        yield return new WaitForSeconds(5f);
    }

    IEnumerator resetChase()
    {
        yield return new WaitForSeconds(2f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(_currentRoutine);
            _currentRoutine = null;
            this.state = States.Chase;
            Debug.Log("Chasing player");
            Vector3 destination = other.gameObject.transform.position;
            Move(destination);
            this.state = States.Wander;
            
        }
    }

    private Vector3 getNextWaypoint()
    {
        return this.enemyWaipoints[waypointIndex++ % enemyWaipoints.Count() ].transform.position;
    }
    void Update()
    {
        if (state == States.Wander)
        {
            if (!enemyWaipoints.Any())
            {
                Vector3 destination = startPosition + new Vector3(Random.Range(-Range, Range), 0, Random.Range(-Range, Range));
                Move(destination);
            }
            else
            {
                if (_currentRoutine == null)
                {
                      currentWaypoint = getNextWaypoint();
                }
                    Move(currentWaypoint);
            }
        }
        else
        {
            chaseDelay();
            this.GetComponent<SphereCollider>().enabled = false;
            resetChase();
            this.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
