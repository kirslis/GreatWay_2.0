using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdlingState : PlayerState
{
    public PlayerIdlingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        return base.Start();
    }

    public override IEnumerator OnLeftMouseDown()
    {
        if (IsClickOnObject())
            PlayerStateMachine.SetNewState(new PlayerDrawingWayState(PlayerStateMachine));
        return base.OnLeftMouseDown();
    }

    private bool IsClickOnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == PlayerStateMachine.GetComponentInChildren<Collider>();
    }

    public override IEnumerator OnSpaceDown()
    {
        Debug.Log("SPACE");
        return base.OnSpaceDown();
    }
}
