using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CInputManager : MonoSingleton<CInputManager>
{
    [SerializeField] 
    private PlayerInput playerInput;

    private InputAction touchPositionAction;

    private void OnDisable()
    {
        this.UnSubscribeInputEventListerner();
    }

    public void StartGame()
    {
        this.Initialize();
    }

    private void Initialize()
    {
        this.touchPositionAction = this.playerInput.actions["MouseTouch"];
        this.SubscribeInputEventListerner();
    }

    private void SubscribeInputEventListerner()
    {
        this.touchPositionAction.performed -= TouchPressed;
        this.touchPositionAction.performed += TouchPressed;
    }

    private void UnSubscribeInputEventListerner()
    {
        this.touchPositionAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPos = context.ReadValue<Vector2>();
        Debug.Log(touchPos);
    }
}
