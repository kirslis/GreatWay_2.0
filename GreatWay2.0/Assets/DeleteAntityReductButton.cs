using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DeleteAntityReductButton : RedactorCreaturesButton
{
    protected override void Awake()
    {
        if (_resourse != null)
            GetComponent<Image>().sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        base.Awake();
    }

    override protected void FixedUpdate()
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
                Debug.Log(mousePos);
              
                    FindObjectOfType<AntityContainer>().DeleteCreature(); ;
                LastTilePos = mousePos;
            }
        }
    }
}
