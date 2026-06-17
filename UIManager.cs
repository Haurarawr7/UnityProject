using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
     [Header("Status Bar")]
    //Tool equip slot on the status bar
    public Image toolEquipSlot; 


    [Header("Inventory System")]
    //The inventory panel
    public GameObject inventoryPanel; 

    //The tool slot UIs
    public InventorySlot[] toolSlots;

    //The item slot UIs
    public InventorySlot[] itemSlots;

    //Item info box
    public Text itemNameText;
    public Text itemDescriptionText; 

    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
    }

    //Render the inventory screen to reflect the Player's Inventory. 
    public void RenderInventory()
    {
        //Get the inventory tool slots from Inventory Manager
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;

        //Get the inventory item slots from Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;

        //Render the Tool section
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //Render the Item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Get Tool Equip from InventoryManager
        ItemData equippedTool = InventoryManager.Instance.equippedTool;

        //Check if there is an item to display
        if (equippedTool != null)
        {
            //Switch the thumbnail over
            toolEquipSlot.sprite = equippedTool.thumbnail;

            toolEquipSlot.gameObject.SetActive(true);

            return;
        }

        toolEquipSlot.gameObject.SetActive(false);

    }

    //Iterate through a slot in a section and display them in the UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        if (slots == null || uiSlots == null)
            return;

        int count = Mathf.Min(slots.Length, uiSlots.Length);
        for (int i = 0; i < count; i++)
        {
            if (uiSlots[i] != null)
                uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //If the panel is hidden, show it and vice versa
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    //Display Item info on the Item infobox
    public void DisplayItemInfo(ItemData data)
    {
        if (itemNameText == null || itemDescriptionText == null)
        {
            Debug.LogWarning("UIManager.DisplayItemInfo: itemNameText or itemDescriptionText is not assigned.");
            return;
        }

        //If data is null, reset
        if(data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
        }

        itemNameText.text = data.name;
        itemDescriptionText.text = data.description; 
    }

    //Update UI when the selected land changes
    public void SetSelectedLand(Land selectedLand)
    {
        if (selectedLand == null)
        {
            Debug.Log("UIManager: selected land cleared.");
            return;
        }

        Debug.Log("UIManager: selected land = " + selectedLand.name);
    }
}
