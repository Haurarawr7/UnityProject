using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum InventoryType
    {
        Item,
        Tool
    }

    ItemData itemToDisplay;
    int slotIndex;

    public Image itemDisplayImage;
    public InventoryType inventoryType;

    public void Display(ItemData itemToDisplay)
    {
        //Check if there is an item to display
        if(itemToDisplay != null)
        {
            //Switch the thumbnail over
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);

            return; 
        }

        itemDisplayImage.gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.InventoryToHand(slotIndex, inventoryType);
    }

    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }


    //Display the item info on the item info box when the player mouses over
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance != null)
            UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    //Reset the item info box when the player leaves
    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIManager.Instance != null)
            UIManager.Instance.DisplayItemInfo(null);
    }
}