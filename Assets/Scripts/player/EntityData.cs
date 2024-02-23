using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Basic Entity")]
public class EntityData : ScriptableObject
{
    [SerializeField] public float maxHP;
    [SerializeField] public float currentHP=100;
    [SerializeField] public float walkModifier;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public bool isAlive;
}
