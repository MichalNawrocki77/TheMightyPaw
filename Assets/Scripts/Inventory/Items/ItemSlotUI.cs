using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    TMP_Text stackCountText;
    [HideInInspector] public Image image;

    /// <summary>
    /// Setting this property will automatically update this ItemSlot's sprite
    /// </summary>
    ItemSO item;
    public ItemSO Item
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
        LoadRefsFromChild();
    }
    public void LoadRefsFromChild()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        stackCountText = image.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        DraggableSlotSprite draggedItem = eventData.pointerDrag.transform.GetComponent<DraggableSlotSprite>();
        
        //swapping children
        transform.GetChild(0).SetParent(draggedItem.currentSlot.transform, false);
        draggedItem.currentSlot.LoadRefsFromChild();
        draggedItem.transform.SetParent(transform, false);
        LoadRefsFromChild();

        //replace Item and StackCount in both ItemSlotsUI's
        //bear in mind that draggedItem.currentSlot is still the "old" slot, I haven't changed it yet
        draggedItem.currentSlot.item = item;
        item = draggedItem.draggedItem;

        draggedItem.currentSlot.StackCount = StackCount;
        StackCount = draggedItem.draggedItemCount;
        

        //Changing parents and replacing old refs to image and StackCountText with refs to new childre
        
        Debug.Log("previous slot: " + draggedItem.currentSlot.Item);
        Debug.Log("previous slot: " + draggedItem.currentSlot.stackCount);

        Debug.Log("new slot: " + Item);
        Debug.Log("new slot: " + stackCount);

        draggedItem.currentSlot = this;
    }
}
