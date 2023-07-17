using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    //Add more item stats HERE, and to update UI read them from here
    public string itemName;
    public Sprite sprite;
    public StackableItemType itemType;
    /// <summary>
    /// If you want to override this method, please bear in mind that Item's OnTriggerEnter2d automatically calls Destroy on itself.
    /// To have all your other OnTriggerEnter functionality, put it before base.OnTriggerEnter call.
    /// </summary>
    /// <param name="collision"></param>
    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PickUpItem(this);
            Destroy(this.gameObject);
        }
    }
}
public enum StackableItemType
{
    Unstackable,
    HealthPotion,
}
