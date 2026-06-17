using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public enum ToolType
    {
        WateringCan,
        Fertilizer,
        Hoe,
        Axe,
        Pickaxe
    }
    public ToolType toolType;
}