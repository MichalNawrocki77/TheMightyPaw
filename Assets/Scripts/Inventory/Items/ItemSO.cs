using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item",menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    //Add more item stats HERE, and to update UI read them from here
    public string itemName;
    public Sprite sprite;
    public StackableItemType itemType;
}
public enum StackableItemType
{
    Unstackable,
    HealthPotion,
}
