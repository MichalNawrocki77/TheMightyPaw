using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<ItemSlotUI> itemSlotsList;

    [SerializeField] RectTransform itemsPanel;
    [SerializeField] RectTransform InventoryPanel;

    int currentItemsCount;

    private void Awake()
    {
        itemSlotsList = new List<ItemSlotUI>();

        FillItemSlotsList();
    }
    void FillItemSlotsList()
    {
        ItemSlotUI temp;
        for (int i = 0; i < itemsPanel.childCount; i++)
        {
            temp = itemsPanel.GetChild(i).GetComponent<ItemSlotUI>();

            temp.inventory = this;
            itemSlotsList.Add(temp);
        }
    }
    public void OpenCloseInventory()
    {
        InventoryPanel.gameObject.SetActive(!InventoryPanel.gameObject.activeSelf);
        Cursor.visible = !Cursor.visible;
        switch (Cursor.visible)
        {
            case true:
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
    public void AddItem(ItemSO newItem, int stackCount)
    {
        switch (newItem.itemType)
        {
            case StackableItemType.Unstackable:
                AddUnstackableItem(newItem);
                break;

            case StackableItemType.HealthPotion:
                break;
        }
        if(newItem.itemType == StackableItemType.Unstackable)
        {
            
        }
        else
        {
            AddStackableItem(newItem, stackCount);
        }
    }
    void AddStackableItem(ItemSO newItem, int stackCount)
    {
        if (CheckIfInventoryFull())
        {
            Debug.Log("too many items in inventory!");
            return;
        }
        //Add the item to already existing stackable item of same kind
        for (int i=0;i<itemSlotsList.Count;i++)
        {
            if (itemSlotsList[i].Item is null)
            {
                continue;
            } 
            if (itemSlotsList[i].Item.itemType == newItem.itemType)
            {
                itemSlotsList[i].StackCount += stackCount;
                return;
            }            
        }
        //if none were found, add the item to first empty slot
        for(int i=0;i<itemSlotsList.Count;i++)
        {
            if (itemSlotsList[i].Item is null)
            {
                itemSlotsList[i].Item = newItem;
                itemSlotsList[i].StackCount = stackCount;
                currentItemsCount++;
                return;
            }
        }
    }
    void AddUnstackableItem(ItemSO newItem)
    {
        if (CheckIfInventoryFull())
        {
            Debug.Log("too many items in inventory!");
            return;
        }
        //I haven't tested this code, it might be buggy
        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if (itemSlotsList[i].Item is null)
            {
                itemSlotsList[i].Item = newItem;
                itemSlotsList[i].StackCount = 0;
                currentItemsCount++;
                return;
            }
        }
        currentItemsCount++;
    }
    public void SwapItemsInSlots(ItemSlotUI newSlot, ItemSlotUI oldSlot)
    {
        int tempStackCount = newSlot.StackCount;
        ItemSO tempItem = newSlot.Item;

        newSlot.StackCount = oldSlot.StackCount;
        newSlot.Item = oldSlot.Item;

        oldSlot.StackCount = tempStackCount;
        oldSlot.Item = tempItem;
    }
    bool CheckIfInventoryFull()
    {
        return currentItemsCount >= itemSlotsList.Count;
    }
    
}
