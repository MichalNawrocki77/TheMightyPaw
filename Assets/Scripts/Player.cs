using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();

        playerControls.StandardActionMap.Enable();
        playerControls.StandardActionMap.Movement.performed += Movement_performed;
        playerControls.StandardActionMap.Dash.performed += Dash_performed;


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
        Debug.Log(movementVectorFromInput.magnitude);
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

    #endregion

}
