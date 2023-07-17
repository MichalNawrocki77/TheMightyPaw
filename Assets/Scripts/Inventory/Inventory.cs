using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static UnityEditor.Progress;

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
    public void AddItem(Item newItem)
    {
        if(newItem.itemType == StackableItemType.Unstackable)
        {
            AddUnstackableItem(newItem);
        }
        else
        {
            AddStackableItem(newItem);
        }
    }
    void AddStackableItem(Item newItem)
    {
        if (CheckIfInventoryFull())
        {
            Debug.Log("too many items in inventory!");
            return;
        }
        for (int i=0;i<currentItemsCount;i++)
        {
            if (itemSlotsList[i].Item.itemType == newItem.itemType)
            {
                itemSlotsList[i].StackCount++;
                currentItemsCount++;
                return;
            }
        }
        itemSlotsList[currentItemsCount].Item = newItem;
        itemSlotsList[currentItemsCount].StackCount = 1;
        currentItemsCount++;
    }
    void AddUnstackableItem(Item newItem)
    {
        if (CheckIfInventoryFull())
        {
            Debug.Log("too many items in inventory!");
            return;
        }
        itemSlotsList[currentItemsCount].Item = newItem;
        currentItemsCount++;
    }
    bool CheckIfInventoryFull()
    {
        return currentItemsCount >= maxCount;
    }
    
}
