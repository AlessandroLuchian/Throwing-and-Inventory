using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Healing Item")]
public class HealingInventoryData : InventoryItemData {
    public float hpToHeal;
    public float timeToHeal;
    public float durationToUse;

}