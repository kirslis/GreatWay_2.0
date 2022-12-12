using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Antity
{
    [SerializeField] GameInterface Interface;

    protected override void SetActive(bool value)
    {
        Debug.Log("SETACTIVE");
        base.SetActive(value);
        GetComponent<PlayerMove>().isActivePlayer = value;
        GetComponent<UiController>().isActive = value;
    }
        
    public void InputMode(bool value)
    {
        Debug.Log("INPUTMODE - " + name);
        GetComponent<PlayerMove>().isActivePlayer = value;
    }
}
