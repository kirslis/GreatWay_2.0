using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Entity
{
    protected override void SetActive(bool value)
    {
        Debug.Log("SETACTIVE AI" + value);
        base.SetActive(value);

        GetComponent<AIMove>().isActivePlayer = value;
        GetComponent<UiController>().isActive = value;
        GetComponent<AIStateMachine>().isActive = value;
    }

    public override void NextTurn()
    {
        base.NextTurn();

        GetComponent<AIMove>().NewTurn();
        StartCoroutine(GetComponent<AIStateMachine>().NewTurn());
    }


}
