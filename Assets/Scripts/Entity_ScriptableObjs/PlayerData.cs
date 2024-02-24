using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Entities/Player Entity")]
public class PlayerData : EntityData
{
    [SerializeField] public float sprintModifier;
    [SerializeField] public float aimRoationSpeed;
    [SerializeField] public float slowWalkModifier;
}
