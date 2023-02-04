using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    protected AIState State;

    public void SetState(AIState state)
    {
        State = state;
        StartCoroutine(State.Start());
    }
}
