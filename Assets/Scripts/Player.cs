using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;

    #region Input related variables

    #region movement

    private Vector2 movementVectorFromInput;
    [SerializeField] float speed;

    #endregion

    #region Dash

    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;

    #endregion

    #endregion

    #region Inventory

    static Inventory inventory;
    [SerializeField] RectTransform itemsPanel;
    [SerializeField] RectTransform InventoryTab;

    [SerializeField] Image image;

    #endregion
    private void Awake()
    {
        playerControls = new PlayerControls();
        inventory= new Inventory();
    }
    void Start()
    {
        playerControls.StandardActionMap.Enable();
        playerControls.StandardActionMap.Movement.performed += Movement_performed;
        playerControls.StandardActionMap.Dash.performed += Dash_performed;
        playerControls.StandardActionMap.OpenCloseInventory.performed += OpenCloseInventory_performed;

        FillItemSlotsList();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
    #region Input Handling
    private void Movement_performed(InputAction.CallbackContext obj)
    {
        movementVectorFromInput = obj.ReadValue<Vector2>();
    }
    private void HandleMovement()
    {
        this.transform.Translate(new Vector3(movementVectorFromInput.x, movementVectorFromInput.y, 0f)* speed * Time.deltaTime);
    }
    private void Dash_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(HandleDash());
    }

    private IEnumerator HandleDash()
    {
        playerControls.StandardActionMap.Movement.Disable();
        playerControls.StandardActionMap.Dash.Disable();

        Vector2 dashVector = movementVectorFromInput.normalized * dashSpeed*Time.deltaTime;
        float currentTime = Time.time;
        while(Time.time - currentTime < dashTime)
        {
            transform.Translate(dashVector);
            yield return new WaitForEndOfFrame();
        }
        movementVectorFromInput = Vector2.zero;
        playerControls.StandardActionMap.Movement.Enable();

        yield return new WaitForSeconds(dashCooldown);
        playerControls.StandardActionMap.Dash.Enable();
    }
    private void OpenCloseInventory_performed(InputAction.CallbackContext obj)
    {
        InventoryTab.gameObject.SetActive(!InventoryTab.gameObject.activeSelf);
        Cursor.visible = !Cursor.visible;
        switch (Cursor.visible)
        {
            case true:
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }

    #endregion

    #region inventory

    void FillItemSlotsList()
    {
        for (int i = 0; i < itemsPanel.childCount; i++)
        {
            inventory.itemSlotsList.Add(itemsPanel.GetChild(i).GetComponent<ItemSlotUI>());
        }
    }
    public void PickUpItem(ItemSO newItem, int stackCount)
    {
        inventory.AddItem(newItem, stackCount);
        Debug.Log("Picked up HP pot!!  " +  newItem.itemType);
    }

    #endregion

}
