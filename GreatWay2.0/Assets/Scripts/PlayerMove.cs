using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : Move
{
    private bool IsLooking;
    private bool IsHold;
    private bool IsMouseDown;
    private GameObject Spirit;
    private PlayerInputAction Input;

    protected override void Awake()
    {
        base.Awake();

        Spirit = new GameObject();
        Spirit.transform.parent = transform;
        Spirit.name = gameObject.name + "_spirit";
        Spirit.AddComponent<SpriteRenderer>();
        Spirit.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        Spirit.GetComponent<SpriteRenderer>().color = Color.red;
        Spirit.SetActive(false);

        Input = new PlayerInputAction();
        Input.Enable();
        Input.MoveActions.LookOut.performed += context => { if (IsClickOnObject()) LookOut(); };
        Input.MoveActions.RefreshPath.performed += context => { if (IsLooking) AbortMoving(); };

        Input.MoveActions.HoldToMove.performed += context =>
        {
            //IsMouseDown = !IsMouseDown;
            //if (IsClickOnObject() || IsHold)
            //{
            //    IsHold = IsMouseDown;
            //    if (IsHold)
            //        StartDrawPath();
            //    else if (GridContainer.countOfPassedTiles != 0)
            //        EndDrawPath();
            //}
        };
    }

    public override bool isActivePlayer
    {
        set
        {
            base.isActivePlayer = value;
            if (value)
                Input.MoveActions.Enable();
            else
                Input.MoveActions.Disable();
        }
    }

    private void OnDestroy()
    {
        Input.Disable();
    }

    private bool IsClickOnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == gameObject.GetComponent<Entity>().collider3d;
    }

    private void LookOut()
    {
        Debug.Log(CurrentSpeed);
        GridContainer.LightUpWaisWrapped(transform.position, currentSpeed);
        IsLooking = true;
    }

    protected override void AbortMoving()
    {
        base.AbortMoving();

        Spirit.gameObject.SetActive(false);
        GridContainer.ResetPath();

        CurrentSpeed = LeftSpeed;
        IsLooking = false;
        IsHold = false;
    }

    private int GetMaxDelta(Vector2 Pos)
    {
        if (GridContainer.countOfPassedTiles == 0)
            return (int)Mathf.Abs(Pos.x - transform.position.x) + (int)Mathf.Abs(Pos.y - transform.position.y);

        return (int)Mathf.Abs(Pos.x - GridContainer.passedTiles[GridContainer.countOfPassedTiles - 1].transform.position.x) + (int)Mathf.Abs(Pos.y - GridContainer.passedTiles[GridContainer.countOfPassedTiles - 1].transform.position.y);
    }

    private void Update()
    {
        if (IsHold && (currentSpeed > 0 || GridContainer.countOfPassedTiles > 0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            MousePos = new Vector2((int)(MousePos.x + 0.5), (int)(MousePos.y + 0.5));
            MousePos = new Vector2(MousePos.x >= 0 ? MousePos.x : 0, MousePos.y >= 0 ? MousePos.y : 0);
            MousePos = new Vector2(MousePos.x < GridContainer.sizeX ? MousePos.x : GridContainer.sizeX - 1, MousePos.y < GridContainer.sizeY ? MousePos.y : GridContainer.sizeY - 1);

            if (!MousePos.Equals(Spirit.transform.position) && GetMaxDelta(MousePos) == 1)
            {
                if (GridContainer.IsCellpasseble(this, MousePos))
                {
                    Spirit.transform.position = MousePos;
                    GridContainer.AddCellToPassed(this, MousePos);
                }
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -2 + YStep * transform.position.y);
    }

    private void StartDrawPath()
    {
        Spirit.SetActive(true);
        LeftSpeed = CurrentSpeed;
        Spirit.transform.position = transform.position;
    }

    private void EndDrawPath()
    {
        Spirit.SetActive(false);
        MoveToEndPoint();
    }

    private void MoveToEndPoint()
    {
        List<BasicTile> WalkPoints = GridContainer.passedTiles;
        StartCoroutine(MoveCourutine(WalkPoints, GridContainer.countOfPassedTiles));
    }



    public void Dash()
    {
        Debug.Log("speed before = " + CurrentSpeed + " " + MaxSpeed);

        CurrentSpeed += MaxSpeed;

        Debug.Log("speed after = " + CurrentSpeed + " " + MaxSpeed);

    }

}
