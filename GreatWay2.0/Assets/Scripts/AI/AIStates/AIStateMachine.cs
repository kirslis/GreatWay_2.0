using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    [SerializeField] protected AIState _idleState;
    [SerializeField] protected AIState _seekingState;
    [SerializeField] protected AIState _battleState;
    [SerializeField] protected AIState _wonderingState;

    protected List<DataTypeHolderScript.enemyInList> EnemyList = new List<DataTypeHolderScript.enemyInList>();
    protected AIState State;
    protected EntityContainer Container;
    protected bool IsActive;

    public List<DataTypeHolderScript.enemyInList> enemyList { get { Debug.Log("Get Enemy list " + this); return EnemyList; } set { Debug.Log("enemy list setten"); EnemyList = value; } }
    public AIState idleState { get { return _idleState; } }
    public AIState seekingState { get { return _seekingState; } }
    public AIState battleState { get { return _battleState; } }
    public AIState wonderingState { get { return _wonderingState; } }
    public bool isActive { set { IsActive = value; } }


    public IEnumerator SetState(AIState state)
    {
        StopCoroutine("TryToAct");
        //StopAllCoroutines();

        List<KeyValuePair<Entity, BasicTile>> VisibleEntities;
        if (State != null)
        {
            yield return State.End();
            VisibleEntities = State.visibleEntities;
            Destroy(State.gameObject);
        }
        else
            VisibleEntities = new List<KeyValuePair<Entity, BasicTile>>();

        State = Instantiate(state);
        State.Init(this, VisibleEntities);

        Debug.Log("Enter in " + State + " state");
        StartCoroutine(State.StateStart());
        Debug.Log("Enter in " + State + " state end");


        if (IsActive)
        {
            yield return NewTurn();
            Debug.Log("New turn?");
        }

        Debug.Log("Try look out");
        State.TakeALook();
        Debug.Log("Enter state end");
    }

    public virtual IEnumerator NewTurn()
    {
        Debug.Log("Start new turn");
        yield return State.NewTurn();
        Debug.Log("Check");
        StartCoroutine(TryToAct());
        yield break;
    }

    public void EndOfTurn()
    {
        StopCoroutine("TryToAct");
        Debug.Log("End by stateMachine" + State);
        State.EndOfTurn();
        Container.NextTurn();
    }

    private void Awake()
    {
        Debug.Log("StateMachineAwake");
        Container = FindObjectOfType<EntityContainer>();
        StartCoroutine(SetState(_idleState));
    }

    public IEnumerator TryToAct()
    {

        AIState startState = State;
        State.TakeALook();
        Debug.Log(State + " " + startState);
        if (startState == State)
        {
            if (State.DoesItNeedToMove())
            {
                Debug.Log("Need to move check");
                yield return State.MoveToTaget();
                StartCoroutine(TryToAct());
            }

            else if (State.ActionCheck())
            {
                yield return State.TryToAct();
                StartCoroutine(TryToAct());
            }

            else
            {
                Debug.Log("CALL END");
                EndOfTurn();
                yield break;
            }
        }
        else
            Debug.Log("State changed");
    }

    public void LookOut()
    {
        if (!IsActive && State != null)
            State.TakeALook();
    }

    public List<DataTypeHolderScript.enemyInList> GetEnemyList()
    {
        return EnemyList;
    }

    public void AddEnemyToList(Entity enemy, int damage, int heal)
    {
        for (int i = 0; i < EnemyList.Count; i++)
            if (EnemyList[i].enemy == enemy)
            {
                DataTypeHolderScript.enemyInList curentEnemy = EnemyList[i];

                curentEnemy.summaryDamage += damage;
                curentEnemy.summaryHeal += heal;

                EnemyList[i] = curentEnemy;
                return;
            }

        Debug.Log("Adding enemy to list " + enemy.name);
        EnemyList.Add(new DataTypeHolderScript.enemyInList(enemy, damage, heal));
    }

    public void AddEnemyToList(Entity enemy)
    {
        for (int i = 0; i < EnemyList.Count; i++)
            if (EnemyList[i].enemy == enemy)
                return;

        Debug.Log("Adding enemy to list " + enemy.name);
        EnemyList.Add(new DataTypeHolderScript.enemyInList(enemy, 0, 0));
    }
}


