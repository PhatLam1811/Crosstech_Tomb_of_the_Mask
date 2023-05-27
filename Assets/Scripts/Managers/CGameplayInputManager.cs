using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum SwipeDirection
{
    UP = 0,
    LEFT = 1,
    DOWN = 2,
    RIGHT = 3
}

public class CGameplayInputManager : MonoSingleton<CGameplayInputManager>
{
    public PlayerInput playerInput;

    [SerializeField] private float maxSwipeTime = 1.0f;
    [SerializeField] private float minSwipeDistance = 2.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float swipeAngleThreshold = 0.75f;

    private InputAction screenTouchAction;
    private InputAction screenHoldAction;

    private UnityAction<SwipeDirection> onPlayerSwipeCallback;

    private Vector3 _startTouchPos = Vector3.negativeInfinity;
    private Vector3 _endTouchPos = Vector3.negativeInfinity;

    private const string SCREEN_TOUCH_ACTION = "ScreenTouch";
    private const string SCREEN_HOLD_ACTION = "ScreenHold";

    private new void Awake()
    {
        this.InitInputActions();
    }

    private void OnEnable()
    {
        this.AssignInputEventListerners();
    }

    private void OnDisable()
    {
        this.UnAssignInputEventListeners();
    }

    private void InitInputActions()
    {
        this.screenTouchAction = this.playerInput.actions[SCREEN_TOUCH_ACTION];
        this.screenHoldAction = this.playerInput.actions[SCREEN_HOLD_ACTION];
    }

    private void AssignInputEventListerners()
    {
        this.screenTouchAction.performed -= this.OnScreenTouchCallback;
        this.screenTouchAction.performed += this.OnScreenTouchCallback;

        this.screenHoldAction.performed -= this.OnScreenHoldCallback;
        this.screenHoldAction.performed += this.OnScreenHoldCallback;
    }

    private void UnAssignInputEventListeners()
    {
        this.screenTouchAction.performed -= this.OnScreenTouchCallback;
        this.screenHoldAction.performed -= this.OnScreenHoldCallback;
    }

    private void OnScreenTouchCallback(InputAction.CallbackContext context)
    {
        float isReleased = context.ReadValue<float>();
        // Debug.Log("On touch = " + isReleased);

        if (isReleased == 0)
        {
            this.DectectSwipeAction(context.startTime, context.time);
            
            this._startTouchPos = Vector3.negativeInfinity;
            this._endTouchPos = Vector3.negativeInfinity;
        }
    }

    private void OnScreenHoldCallback(InputAction.CallbackContext context)
    {
        Vector2 holdPos = context.ReadValue<Vector2>();
        // Debug.Log("On hold = " + holdPos);

        if (this._startTouchPos.Equals(Vector3.negativeInfinity))
        {
            this._startTouchPos = new Vector3(holdPos.x, holdPos.y);
        }
        else
        {
            this._endTouchPos = new Vector3(holdPos.x, holdPos.y);
        }
    }

    private void DectectSwipeAction(double startTime, double time)
    {
        if (this._startTouchPos.Equals(Vector3.negativeInfinity)) return;

        if (time - startTime <= this.maxSwipeTime &&
            Vector3.Distance(this._startTouchPos, this._endTouchPos) >= this.minSwipeDistance)
        {
            this.OnPlayerSwiped();
        }
    }

    private void OnPlayerSwiped()
    {
        Vector3 swipeDirection = (this._endTouchPos - this._startTouchPos).normalized;

        if (Vector3.Dot(Vector3.up, swipeDirection) >= this.swipeAngleThreshold)
        {
            //Debug.Log("Move Up!");
            this.InvokeOnPlayerSwipedCallback(SwipeDirection.UP);
        }
        else if (Vector3.Dot(Vector3.left, swipeDirection) >= this.swipeAngleThreshold)
        {
            //Debug.Log("Move Left!");
            this.InvokeOnPlayerSwipedCallback(SwipeDirection.LEFT);
        }
        else if (Vector3.Dot(Vector3.down, swipeDirection) >= this.swipeAngleThreshold)
        {
            //Debug.Log("Move Down!");
            this.InvokeOnPlayerSwipedCallback(SwipeDirection.DOWN);
        }
        else if (Vector3.Dot(Vector3.right, swipeDirection) >= this.swipeAngleThreshold)
        {
            //Debug.Log("Move Right!");
            this.InvokeOnPlayerSwipedCallback(SwipeDirection.RIGHT);
        }
    }

    public void AssignOnPlayerSwipedCallback(UnityAction<SwipeDirection> callback)
    {
        this.onPlayerSwipeCallback -= callback;
        this.onPlayerSwipeCallback += callback;
    }

    public void UnAssignOnPlayerSwipedCallback(UnityAction<SwipeDirection> callback)
    {
        this.onPlayerSwipeCallback -= callback;
    }

    public void InvokeOnPlayerSwipedCallback(SwipeDirection swipeDirection)
    {
        this.onPlayerSwipeCallback?.Invoke(swipeDirection);
    }
}