using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RedactorTileButton : BasicReductorButton, IPointerEnterHandler, IPointerExitHandler
{

    protected override void Awake()
    {
        GetComponent<Image>().sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        base.Awake();   
    }

    private void FixedUpdate()
    {
        if (IsMouseDown)
            Map.ChangeTile(_resourse.GetComponent<BasicTile>());
    }
}
