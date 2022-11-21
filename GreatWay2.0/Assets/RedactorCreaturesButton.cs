using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RedactorCreaturesButton : BasicReductorButton, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 LastTilePos = new Vector2();

    protected override void Awake()
    {
        GetComponent<Image>().sprite = _resourse.GetComponent<CharacterStats>().image;
        base.Awake();
    }


    private void FixedUpdate()
    {
        if (IsMouseDown && IsSingleReducted)
        {
            IsSingleReducted = false;
            Vector2 mousePos = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos = new Vector2((int)(mousePos.x + 0.5f), (int)(mousePos.y + 0.5f));

            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);

            if (hit.collider != null)
                AbortReduct();

            else if (!LastTilePos.Equals(mousePos))
            {
                if (!Map.TryAddCreature(_resourse.GetComponent<Antity>(), mousePos))
                    ErrorMassage("something interferes");
                LastTilePos = mousePos;
            }
        }
    }

    public void ErrorMassage(string s)
    {
        _menu.ThrowErrorText(s);
    }
}
