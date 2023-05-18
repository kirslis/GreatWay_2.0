using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AIGeneralBattleState : AIState
{
    [SerializeField][Range(0, 10)] protected int damageValueCoeff;
    [SerializeField][Range(0, 10)] protected int healValueCoeff;

    protected BasicTile targetTile = null;
    protected List<DataTypeHolderScript.enemyInList> enemyList;

    protected virtual void GetTarget()
    {

    }

    protected virtual bool comp(DataTypeHolderScript.enemyInList a, DataTypeHolderScript.enemyInList b)
    {
        return a.priorityRate < b.priorityRate;
    }

    protected virtual void SortPriorityList()
    {
        enemyList = aIStateMachine.enemyList;

        for (int i = 0; i < enemyList.Count; i++)
        {
            DataTypeHolderScript.enemyInList enemyInList = new DataTypeHolderScript.enemyInList();
            enemyInList.enemy = enemyList[i].enemy;
            enemyInList.summaryDamage = enemyList[i].summaryDamage;
            enemyInList.summaryHeal = enemyList[i].summaryHeal;
            
            enemyInList.pathLength = (Move.GetShortestPathLength(map.GetTile(enemyInList.enemy.transform.position)));
            enemyInList.priorityRate = -enemyInList.pathLength + damageValueCoeff * enemyList[i].summaryDamage + healValueCoeff * healValueCoeff;
            enemyList[i] = enemyInList;
        }

        List<DataTypeHolderScript.enemyInList> sortedList = new List<DataTypeHolderScript.enemyInList>();

        foreach (DataTypeHolderScript.enemyInList enemy in enemyList)
        {
            Debug.Log(enemy.enemy);
            Debug.Log(enemy.enemy.transform.position);
            Debug.Log(enemy.pathLength);
            Debug.Log(enemy.priorityRate);
        }

        for (int i = 0; i < enemyList.Count; i++)
        {
            sortedList.Add(enemyList[i]);
            int j = sortedList.Count - 2;
            while (j >= 0 && comp(sortedList[j], enemyList[i]))
            {
                sortedList[j + 1] = enemyList[j];
                j--;
            }

            sortedList[j + 1] = enemyList[i];
        }

        aIStateMachine.enemyList = sortedList;
    }

    public override IEnumerator NewTurn()
    {
        Debug.Log("New turn zombie");
        targetTile = null;
        yield return base.NewTurn();
    }
}
