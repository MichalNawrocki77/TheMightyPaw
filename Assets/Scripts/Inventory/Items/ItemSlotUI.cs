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
    public Inventory inventory;

    TMP_Text stackCountText;
    [HideInInspector] public Image spriteImageComponent;
    [SerializeField] Image imageComponent;
    [SerializeField] Selectable selectableComponent;

    [HideInInspector]
    public DraggableSlotSprite currentItem;

    
    ItemSO item;
    /// <summary>
    /// Setting this property will automatically update this ItemSlot's sprite
    /// </summary>
    public ItemSO Item
    {
        get { return item; }
        set
        {
            item = value;
            if(item is null)
            {
                this.spriteImageComponent.sprite = null;
            }
            this.spriteImageComponent.sprite = item.sprite;
        }
    }

    int stackCount;
    /// <summary>
    /// Setting this property will automatically update this ItemSlot's stackCountText.
    /// </summary>
    public int StackCount
    {
        get { return stackCount; }
        set
        {
            stackCount = value;
            if(value == 0)
            {
                this.stackCountText.text = string.Empty;
                return;
            }
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

        inventory.SwapItemsInSlots(this, draggedItem.currentSlot);
        
        Debug.Log("OnDrop");

        

        selectableComponent.Select();
    }
    
    #endregion
}
