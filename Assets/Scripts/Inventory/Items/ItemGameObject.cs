using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public ItemSO item;
    [SerializeField] int stackCount = 1;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
    }
    /// <summary>
    /// If you want to override this method, please bear in mind that Item's OnTriggerEnter2d automatically calls Destroy on itself.
    /// To have all your other OnTriggerEnter functionality, put it before base.OnTriggerEnter call.
    /// </summary>
    /// <param name="collision"></param>
    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PickUpItem(item, stackCount);
            Destroy(this.gameObject);
        }
    }
}
