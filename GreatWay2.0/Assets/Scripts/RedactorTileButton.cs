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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos = new Vector2((int)(mousePos.x + 0.5f), (int)(mousePos.y + 0.5f));

            if (!LastTilePos.Equals(mousePos))
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