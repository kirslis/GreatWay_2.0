using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Antity
{
    protected override void SetActive(bool value)
    {
        base.SetActive(value);
        GetComponent<PlayerMove>().isActivaPlayer = value;
        GetComponent<UiController>().isActive = value;
    }
}
