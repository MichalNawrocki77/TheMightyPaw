using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory
{
    public List<ItemSlotUI> itemSlotsList;

    //to not iterate every single time over the whole itemSlotsList
    int currentItemsCount;
    int maxCount = 16;
    public Inventory()
    {
        itemSlotsList= new List<ItemSlotUI>();
    }
    //void UpdateSingleItemInInventory(int itemIndex)
    //{
    //    itemSlotsList[itemIndex].image.sprite = itemSlotsList[itemIndex].ItemUI.item.sprite;
    //    itemSlotsList[itemIndex].stackCountText = itemSlotsList[itemIndex].ItemUI.item.;
    //}
    public void AddItem(ItemSO newItem, int stackCount)
    {
        if(newItem.itemType == StackableItemType.Unstackable)
        {
            AddUnstackableItem(newItem);
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
        itemSlotsList[currentItemsCount].Item = newItem;
        itemSlotsList[currentItemsCount].StackCount = null;
        currentItemsCount++;
    }
    bool CheckIfInventoryFull()
    {
        return currentItemsCount >= maxCount;
    }
    
}
