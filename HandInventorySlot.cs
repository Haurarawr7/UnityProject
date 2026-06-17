using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandInventorySlot : InventorySlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Move item from hand to inventory
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.HandToInventory(inventoryType);
    }
}