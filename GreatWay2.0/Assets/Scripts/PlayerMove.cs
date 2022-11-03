using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] int _speed = 6;

    private int MaxSpeed;
    private int CurrentSpeed;
    private int LeftSpeed;

    private GlobalVisionController GlobalVision;
    private bool IsLooking;
    private bool IsHold;
    private bool IsMouseDown;
    private GameObject Spirit;
    private PlayerInputAction Input;
    private GridContainer GridContainer;
    private float YStep;

    public float yStep { set { YStep = value; } }
    public int currentSpeed { get { return CurrentSpeed; } set { CurrentSpeed = value; } }
    public bool isActivaPlayer
    {
        set
        {
            if (value)
            {
                Input.MoveActions.Enable();
                CurrentSpeed = MaxSpeed;
            }
            else
            {
                Input.MoveActions.Disable();
                AbortMoving();
            }
            GridContainer.GetTile(transform.position).isPasseble = value;
        }
    }

    private void Awake()
    {
        MaxSpeed = _speed;
        CurrentSpeed = _speed;
        LeftSpeed = _speed;

        GlobalVision = FindObjectOfType<GlobalVisionController>();

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
            IsMouseDown = !IsMouseDown;
            if (IsClickOnObject() || IsHold)
            {
                IsHold = IsMouseDown;
                if (IsHold)
                    StartDrawPath();
                else if (GridContainer.countOfPassedTiles != 0)
                    EndDrawPath();
            }
        };

        GridContainer = FindObjectOfType<GridContainer>();
    }

    private void OnDestroy()
    {
        Input.Disable();
    }

    private bool IsClickOnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == gameObject.GetComponent<Antity>().collider3d;
    }

    private void LookOut()
    {
        GridContainer.LightUpWaisWrapped(transform.position, currentSpeed);
        IsLooking = true;
    }

    private void AbortMoving()
    {
        GridContainer.ResetLightedTiles();
        Spirit.gameObject.SetActive(false);
        GridContainer.ResetPath();
        CurrentSpeed = LeftSpeed;
        IsLooking = false;
    }

    private void Update()
    {
        if (IsHold)
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            MousePos = new Vector2((int)(MousePos.x + 0.5), (int)(MousePos.y + 0.5));
            MousePos = new Vector2(MousePos.x >= 0 ? MousePos.x : 0, MousePos.y >= 0 ? MousePos.y : 0);
            MousePos = new Vector2(MousePos.x < GridContainer.sizeX ? MousePos.x : GridContainer.sizeX - 1, MousePos.y < GridContainer.sizeY ? MousePos.y : GridContainer.sizeY - 1);

            if (!MousePos.Equals(Spirit.transform.position))
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

    private IEnumerator MoveCourutine(List<BasicTile> WalkPoints, int Count)
    {
        Input.MoveActions.Disable();
        GridContainer.ResetLightedTiles();
        
        for (int i = 0; i < Count; i++)
        {
            while (!((Vector2)transform.position).Equals(WalkPoints[i].transform.position))
            {
                transform.position = Vector2.MoveTowards(transform.position, WalkPoints[i].transform.position, 3*Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, -2 + YStep * transform.position.y);

                yield return null;
            }

            GlobalVision.AllLookOut();
        }

        GridContainer.ResetPath();

        Input.MoveActions.Enable();
    }

}
