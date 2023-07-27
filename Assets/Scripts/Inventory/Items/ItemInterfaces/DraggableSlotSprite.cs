using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DraggableSlotSprite : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [HideInInspector] public ItemSlotUI currentSlot;
    Image imageComponent;
    public ItemSO draggedItem;
    public int? draggedItemCount;
    private void Awake()
    {
        currentSlot = transform.parent.GetComponent<ItemSlotUI>();
        imageComponent = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        draggedItem = currentSlot.Item;
        draggedItemCount = currentSlot.StackCount;

        imageComponent.raycastTarget = false;
        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //this event triggers after ItemSlotUI's OnDrop
        transform.SetParent(currentSlot.transform);
        currentSlot.LoadRefsFromChild();
        if (draggedItem is not null)
        {
            currentSlot.Item = draggedItem;
        }
        currentSlot.StackCount = draggedItemCount;

        transform.position = currentSlot.transform.position;
        imageComponent.raycastTarget = true;
        Debug.Log("EndDrag");
    }
    
}
