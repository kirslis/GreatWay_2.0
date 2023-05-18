using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AISkeletonBattleState : AIZombieBattleState
{
    protected override void SortPriorityList()
    {
        //List<AIStateMachine.enemyInList> enemyList = aIStateMachine.GetEnemyList();

        ////for (int i = 0; i < enemyList.Count; i++)
        ////    enemyList[i].SetPriority(enemyList[i].summaryDamage * damageValueCoeff + enemyList[i].summaryHeal * healValueCoeff);

        //int iEnd = enemyList.Count - 1;
        //bool haveChanged = false;

        //while (iEnd > 0)
        //{
        //    haveChanged = false;

        //    for (int j = 0; j < iEnd; j++)
        //        if (enemyList[j].priorityRate < enemyList[j + 1].priorityRate)
        //        {
        //            AIStateMachine.enemyInList t = enemyList[j];
        //            enemyList[j] = enemyList[j + 1];
        //            enemyList[j + 1] = t;
        //            haveChanged = true;
        //        }

        //    if (!haveChanged)
        //        return;

        //    iEnd--;
        //}
    }

    protected override void GetTarget()
    {
        TakeALook();
        Debug.Log("Seek");
        List<KeyValuePair<Entity, BasicTile>> visibleEnemies = new List<KeyValuePair<Entity, BasicTile>>();

        foreach (KeyValuePair<Entity, BasicTile> entity in VisibleEntities)
            if (entity.Key.tag != aIStateMachine.tag)
            {
                Debug.Log(entity.Key);
                visibleEnemies.Add(entity);
            }


        if (visibleEnemies.Count == 0)

            targetTile = null;

        while (targetTile == null)
        {
            KeyValuePair<Entity, BasicTile> closestTargetTile = visibleEnemies[0];
            int smallestLength = Move.GetShortestPathLength(closestTargetTile.Value);

            foreach (KeyValuePair<Entity, BasicTile> targetTile in visibleEnemies)
                if (Move.GetShortestPathLength(targetTile.Value) < smallestLength)
                {
                    smallestLength = Move.GetShortestPathLength(targetTile.Value);
                    closestTargetTile = targetTile;
                }

            if (!IsTileAvailable(closestTargetTile))
            {
                ClearUnAvailableTiles(visibleEnemies);
                if (visibleEnemies.Count == 0)
                    targetTile = closestTargetTile.Value;
            }
            else
                targetTile = closestTargetTile.Value;
        }

        Debug.Log(targetTile + " " + targetTile.transform.position);
        base.GetTarget();
    }
}
