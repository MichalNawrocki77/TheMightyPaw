using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    TMP_Text stackCountText;
    Image image;

    /// <summary>
    /// Setting this property will automatically update this ItemSlot's sprite
    /// </summary>
    Item item;
    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            this.image.sprite = item.sprite;
        }
    }

    int? stackCount;
    /// <summary>
    /// Setting this property will automatically update this ItemSlot's stackCountText.
    /// </summary>
    public int? StackCount
    {        
        get { return stackCount; }
        set
        {
            stackCount = value;
            this.stackCountText.text = stackCount.ToString();
        }
    }
    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        stackCountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }
}
