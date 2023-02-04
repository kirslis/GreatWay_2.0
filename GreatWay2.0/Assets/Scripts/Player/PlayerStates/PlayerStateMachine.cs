using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine : MonoBehaviour
{
    protected PlayerState PlayerState;

    public void SetState(PlayerState State)
    {
        PlayerState = State;
        StartCoroutine(PlayerState.Start());
    }
}
