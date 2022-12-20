using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Antity
{

    private void Awake()
    {
        if (FindObjectOfType<MapRedactorMenu>().isActiveAndEnabled)
            GetComponent<AIMove>().CreatePatrouleTrail();
    }

    protected override void SetActive(bool value)
    {
        Debug.Log("SETACTIVE");
        base.SetActive(value);
        if (value)
            GetComponent<AIStateMachine>().Active(); ;
        GetComponent<UiController>().isActive = value;
    }
}
