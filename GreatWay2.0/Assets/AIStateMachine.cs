using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    [SerializeField] AIMove _AIMove;

    private void Awake()
    {
        
    }

    public void Active()
    {
        _AIMove.MoveToNextPatrolePoint();
    }
}
