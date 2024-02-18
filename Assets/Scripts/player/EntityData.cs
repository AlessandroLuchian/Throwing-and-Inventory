using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    [SerializeField] public float maxHP;
    [SerializeField] public float currentHP=100;
    [SerializeField] public float walkModifier;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public bool isAlive;
}
