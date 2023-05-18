using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawingWayState : PlayerState
{
    public PlayerDrawingWayState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        PlayerStateMachine.GetComponent<PlayerMove>().StartDrawPath();
        return base.Start();
    }

    public override IEnumerator OnLeftMouseUp()
    {
        Debug.Log("TRY TO STOP");
        if (PlayerStateMachine.GetComponent<PlayerMove>().isLooking)
        {
            yield return PlayerStateMachine.GetComponent<PlayerMove>().EndDrawPath();
            PlayerStateMachine.SetNewState(new PlayerIdlingState(PlayerStateMachine));
        }
    }
}
