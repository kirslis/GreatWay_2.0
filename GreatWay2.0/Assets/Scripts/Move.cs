using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] protected int _speed = 6;

    private GlobalVisionController GlobalVision;

    protected Animator Anim;
    protected int MaxSpeed;
    protected int CurrentSpeed;
    protected int LeftSpeed;
    protected float YStep;
    protected GridContainer GridContainer;

    public float yStep { set { YStep = value; } }
    public int currentSpeed { get { return CurrentSpeed; } set { CurrentSpeed = value; } }

    public virtual bool isActivePlayer
    {
        set
        {
            if (value && FindObjectOfType<EntityContainer>().currentPlayer == GetComponent<Entity>())
            {
                GridContainer.GetTile(transform.position).isPasseble = true;
            }
            else
            {
                GridContainer.GetTile(transform.position).isPasseble = false;
                AbortMoving();
            }
        }
    }

    public void NewTurn()
    {
        CurrentSpeed = MaxSpeed;
        LeftSpeed = CurrentSpeed;
        Debug.Log("NExtTurn speed = " + CurrentSpeed);
    }

    protected virtual void Awake()
    {
        GlobalVision = FindObjectOfType<GlobalVisionController>();
        Anim = GetComponent<Animator>();
        GridContainer = FindObjectOfType<GridContainer>();
        MaxSpeed = _speed;
        CurrentSpeed = _speed;
        LeftSpeed = _speed;
    }

    protected virtual void AbortMoving()
    {
        GridContainer.ResetLightedTiles();

    }

    protected IEnumerator MoveCourutine(List<BasicTile> WalkPoints, int Count)
    {
        GetComponent<PlayerController>().InputMode(false);

        GridContainer.ResetLightedTiles();

        for (int i = 1; i < Count; i++)
            yield return MoveToPointCourutine(WalkPoints[i]);

        AbortMoving();

        GetComponent<PlayerController>().InputMode(true);

    }

    protected IEnumerator MoveToPointCourutine(BasicTile WalkPoint)
    {
        Anim.SetBool("IsRunning", true);
        if (WalkPoint.transform.position.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (WalkPoint.transform.position.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;

        while (!((Vector2)transform.position).Equals(WalkPoint.transform.position))
        {
            transform.position = Vector2.MoveTowards(transform.position, WalkPoint.transform.position, 3 * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -2 + YStep * transform.position.y);

            yield return null;
        }
        LeftSpeed -= WalkPoint.currentPathCost;
        GlobalVision.AllLookOut();
        Anim.SetBool("IsRunning", false);
    }
}
