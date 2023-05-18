using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New General Seeking State", menuName = "AI States/General/AI General Seeking State", order = 51)]
public class AIGeneralSeekingState : AIState
{
    private KeyValuePair<Entity, BasicTile> seekTarget;

    public override void Init(AIStateMachine stateMachine, List<KeyValuePair<Entity, BasicTile>> VisibleEntities)
    {
        base.Init(stateMachine, VisibleEntities);
        foreach (KeyValuePair<Entity, BasicTile> target in VisibleEntities)
            if (target.Key.tag != aIStateMachine.tag)
            {
                seekTarget = target;
                Debug.Log(map + " target " + seekTarget);
                return;
            }
    }

    public override void TakeALook()
    {
        Debug.Log("Seeking look");
        if (EnemyCheck(aIVisionConroller))
        {
            StopCoroutine("MoveToTarget");
            StartCoroutine(aIStateMachine.SetState(aIStateMachine.battleState));
            return;
        }
        else if (map.GetTile(seekTarget.Value.transform.position) ==
            map.GetTile(aIStateMachine.transform.position))
        {
            StartCoroutine(aIStateMachine.SetState(aIStateMachine.wonderingState));
            return;
        }
        base.TakeALook();
    }

    public override bool DoesItNeedToMove()
    {
        Debug.Log("What" + seekTarget);

        return Move.currentSpeed > 0
            && map.GetTile(seekTarget.Value.transform.position) != map.GetTile(aIStateMachine.transform.position);
    }

    public override IEnumerator MoveToTaget()
    {
        yield return Move.MakeAStepTo(seekTarget.Value);
        Debug.Log("STEP DONE");
    }
}
