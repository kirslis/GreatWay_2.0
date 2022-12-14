using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RedactorEnviromentButton : BasicReductorButton, IPointerEnterHandler, IPointerExitHandler
{
    protected Vector2 LastTilePos = new Vector2();
    private float zAngle = 0;
    private GameObject miniObject;
    private GridContainer Grid;

    protected override void Awake()
    {
        Grid = FindObjectOfType<GridContainer>();

        miniObject = new GameObject();
        miniObject.AddComponent<SpriteRenderer>();
        miniObject.GetComponent<SpriteRenderer>().sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        miniObject.transform.localScale = _resourse.transform.localScale;
        miniObject.SetActive(false);
        miniObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        GetComponent<Image>().sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        base.Awake();
        Input.Reduct.Rotate.performed += context => { zAngle += 90; if (zAngle >= 360) zAngle = 0; miniObject.transform.Rotate(0, 0, 90); };
        Input.Reduct.AbortReduct.performed += context => { miniObject.SetActive(false); miniObject.transform.rotation = Quaternion.identity; zAngle = 0; };
    }


    virtual protected void FixedUpdate()
    {
        if (IsReducting)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos = new Vector3((int)(mousePos.x + 0.5f), (int)(mousePos.y + 0.5f), -3);
            if (IsMouseDown)
            {
                Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    AbortReduct();
                }
                else if (!LastTilePos.Equals(mousePos))
                {
                    if (!Map.TryAddEnviroment(_resourse.GetComponent<Enviroment>(), mousePos, zAngle))
                        ErrorMassage("this tile not empty");
                    LastTilePos = mousePos;
                }
            }
            
            miniObject.transform.position = new Vector3(Grid.redactorCursorPos.x, Grid.redactorCursorPos.y, -3);
        }
    }

    public void ErrorMassage(string s)
    {
        _menu.ThrowErrorText(s);
    }

    protected override void AbortReduct()
    {
        miniObject.SetActive(false);
        base.AbortReduct();
    }

    public override void StartReduct()
    {
        miniObject.SetActive(true);
        base.StartReduct();
        MiniTile.gameObject.GetComponent<Image>().enabled = false;
    }
}
