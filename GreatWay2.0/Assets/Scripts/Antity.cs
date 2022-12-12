using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antity : MonoBehaviour
{
    [SerializeField] GameObject Collider3d;
    protected bool IsActivePlayer;

    public bool isActive { get { return IsActivePlayer; } set { SetActive(value); } }
    public BoxCollider collider3d { get { return Collider3d.GetComponent<BoxCollider>(); } }

    virtual protected void SetActive(bool value)
    {
        IsActivePlayer = value;
    }

    virtual public void NextTurn()
    {
        GetComponent<CharacterStats>().NewTurn();
        GetComponent<PlayerMove>().NewTurn();
    }
}
