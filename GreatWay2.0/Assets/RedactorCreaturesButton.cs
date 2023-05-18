using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RedactorCreaturesButton : BasicReductorButton, IPointerEnterHandler, IPointerExitHandler
{
    protected Vector2 LastTilePos = new Vector2();

    protected override void Awake()
    {
        if (_resourse.GetComponent<CharacterStats>() != null)
            GetComponent<Image>().sprite = _resourse.GetComponent<CharacterStats>().image;
        base.Awake();
    }


    virtual protected void FixedUpdate()
    {
        if (IsMouseDown && IsSingleReducted)
        {
            IsSingleReducted = false;
            Vector2 mousePos = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);

            if (hit.collider != null && hit.collider.gameObject.layer == gameObject.layer)
                AbortReduct();

            else if (!LastTilePos.Equals(mousePos))
            {
                Debug.Log(_resourse.GetComponent<Entity>() + " " + Map + " " + mousePos);

                if (!Map.TryAddCreature(_resourse.GetComponent<Entity>(), mousePos))
                {
                    ErrorMassage("something interferes");
                    StartReduct();
                }
                LastTilePos = mousePos;
            }
        }
    }

    public void ErrorMassage(string s)
    {
        _menu.ThrowErrorText(s);
    }
}
