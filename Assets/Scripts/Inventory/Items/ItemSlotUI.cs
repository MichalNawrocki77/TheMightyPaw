using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour,IDropHandler
{
    TMP_Text stackCountText;
    [HideInInspector] public Image spriteImageComponent;
    [SerializeField] Image imageComponent;
    [SerializeField] Selectable selectableComponent;

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
            this.spriteImageComponent.sprite = item.sprite;
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
        spriteImageComponent = transform.GetChild(0).GetComponent<Image>();
        stackCountText = spriteImageComponent.transform.GetChild(0).GetComponent<TMP_Text>();
    }
    #region UIEvents
    public void OnDrop(PointerEventData eventData)
    {
        DraggableSlotSprite draggedItem = eventData.pointerDrag.transform.GetComponent<DraggableSlotSprite>();
        
        Debug.Log("OnDrop");
        DraggableSlotSprite replacedItem = transform.GetChild(0).GetComponent<DraggableSlotSprite>();

        //setting other slot's child (not this one's, explaination in line 79

        //When you drop draggable item on the original slot, you get NullPointerException on this line???????
        //My current idea to fix is to instantiate a new DraggableSlotItem on top of the orignal slot, similar to how Diablo IV handles this functionality????????
        replacedItem.transform.SetParent(draggedItem.currentSlot.transform, false);
        replacedItem.currentSlot = draggedItem.currentSlot;
        replacedItem.currentSlot.LoadRefsFromChild();

        //replace Item and StackCount in the other slot
        //bear in mind that draggedItem.currentSlot is still the "old" slot, I haven't changed it yet
        //that's why I dont replace this slot's item and stackCount, I replace them in OnDragEnd(), since it's there, where I load new refs to children.
        replacedItem.currentSlot.item = replacedItem.draggedItem;
        replacedItem.currentSlot.StackCount = replacedItem.draggedItemCount;

        //setting new parent for dragged item happens in it's own DraggableSlotSprite.OnEndDrag(), because of a bug caused by dropping the item on none of the slots (the parent would not be reassigned, since OnDrop() was not called).
        //LoadRefsFromChild call also happens in OnEndDrag() because of a ChildOutOfBOunds exception. It happened because I was calling LoadRefsFromChild before setting a new child
        draggedItem.currentSlot = this;

        selectableComponent.Select();
    }
    
    #endregion
}
