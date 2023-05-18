using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIGeneralIdlingState : AIState
{
    public override void TakeALook()
    {
        if (EnemyCheck(aIVisionConroller))
            StartCoroutine(aIStateMachine.SetState(aIStateMachine.seekingState));
    }

    public override bool DoesItNeedToMove()
    {
        if (Move.currentSpeed > Move.maxSpeed / 2)
        {
            if (Move.isPatroling && Move.GetNextIdlingTile().isPasseble)
                return true;
        }
        return false;
    }

    public override IEnumerator MoveToTaget()
    {
        if (Move.isPatroling)
            yield return Move.MakeAPatroleStep();

        else
            yield break;
    }
}
