using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DeleteEnviromentReductButton : DeleteAntityReductButton
{
    protected override void Awake()
    {
        base.Awake();
    }

    override protected void FixedUpdate()
    {
        if (IsReducting)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos = new Vector3((int)(mousePos.x + 0.5f), (int)(mousePos.y + 0.5f), -3);
            if (IsMouseDown)
            {
                Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);

                if (hit.collider != null)
                    AbortReduct();

                else if (!LastTilePos.Equals(mousePos))
                {
                    FindObjectOfType<AntityContainer>().DeleteEnviroment();
                    LastTilePos = mousePos;
                }
            }
        }
    }
}
