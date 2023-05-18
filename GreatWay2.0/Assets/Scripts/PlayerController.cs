using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Entity
{
    private PlayerInputAction Input;
    private PlayerStateMachine StateMachine;

    private void Awake()
    {
        StateMachine = GetComponent<PlayerStateMachine>();

        Input = new PlayerInputAction();
        Input.Enable();
        Input.MoveActions.LMBDown.performed+= context =>
        {
            Debug.Log(StateMachine.playerState.OnLeftMouseDown());
            StartCoroutine(StateMachine.playerState.OnLeftMouseDown());
            //if (IsClickOnObject()) LookOut();
        };
        Input.MoveActions.LMBUp.performed += context =>
        {
            StartCoroutine(StateMachine.playerState.OnLeftMouseUp());
        };
       Input.MoveActions.RMB.performed += context =>
        {
            StartCoroutine(StateMachine.playerState.OnRightMouseDown());
            //if (IsLooking) AbortMoving(); 
        };

        Input.MoveActions.Space.performed += context =>
        {
            StartCoroutine(StateMachine.playerState.OnSpaceDown());
            //    //IsMouseDown = !IsMouseDown;
            //    //if (IsClickOnObject() || IsHold)
            //    //{
            //    //    IsHold = IsMouseDown;
            //    //    if (IsHold)
            //    //        StartDrawPath();
            //    //    else if (GridContainer.countOfPassedTiles != 0)
            //    //        EndDrawPath();
            //    //}
        };
    }

    protected override void SetActive(bool value)
    {
        Debug.Log("SETACTIVE" + value);
        base.SetActive(value);
        GetComponent<PlayerMove>().isActivePlayer = value;
        GetComponent<UiController>().isActive = value;
    }

    public void InputMode(bool value)
    {
        Debug.Log("INPUTMODE - " + name);
        GetComponent<PlayerMove>().isActivePlayer = value;
    }

    public override void NextTurn()
    {
        base.NextTurn();
        GetComponent<PlayerMove>().NewTurn();
    }
}
