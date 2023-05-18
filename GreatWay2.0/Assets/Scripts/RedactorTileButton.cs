using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RedactorTileButton : BasicReductorButton, IPointerEnterHandler, IPointerExitHandler
{

    private Vector2 LastTilePos = new Vector2();

    protected override void Awake()
    {
        GetComponent<Image>().sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        base.Awake();
    }

    private void FixedUpdate()
    {
        if (IsMouseDown)
        {
            Vector2 mousePos = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);

            if (hit.collider != null && hit.collider.gameObject.layer == gameObject.layer)
            {
                AbortReduct();
                Debug.Log(hit.collider.gameObject);
            }
            else if (!LastTilePos.Equals(mousePos))
            {
                if (!Map.TryChangeTile(_resourse.GetComponent<BasicTile>()))
                    ErrorMassage("this tile not empty");
                LastTilePos = mousePos;
            }
            Debug.Log(mousePos);
        }
    }

    public void ErrorMassage(string s)
    {
        _menu.ThrowErrorText(s);
    }
}
