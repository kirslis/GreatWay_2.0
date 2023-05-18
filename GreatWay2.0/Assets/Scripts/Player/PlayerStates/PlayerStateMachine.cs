using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState PlayerState = null;

    public PlayerState playerState { get { return PlayerState; } }

    private void Awake()
    {
        PlayerState = new PlayerIdlingState(this);
        PlayerState.Start();
    }

    public void SetNewState(PlayerState state)
    {
        Debug.Log(name + " Set new State - " + state);
        PlayerState.End();
        PlayerState = state;
        PlayerState.Start();
    }
}
