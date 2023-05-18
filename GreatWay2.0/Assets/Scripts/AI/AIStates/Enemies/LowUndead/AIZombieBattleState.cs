using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieBattleState : AIGeneralBattleState
{
    public override void TakeALook()
    {
        EnemyCheck(aIVisionConroller);
        List<BasicTile> VisibleTiles = aIVisionConroller.visibleTiles;

        Debug.Log("visible tiles " + VisibleTiles.Count);
        List<KeyValuePair<Entity, BasicTile>> newVisibleEntities = new List<KeyValuePair<Entity, BasicTile>>();
        foreach (BasicTile tile in VisibleTiles)
            if (tile.GetComponent<TileContainer>().entityOnTile != null && tile.GetComponent<TileContainer>().entityOnTile.tag != aIStateMachine.tag)
                newVisibleEntities.Add(new KeyValuePair<Entity, BasicTile>(tile.GetComponent<TileContainer>().entityOnTile, tile));

        if (newVisibleEntities.Count == 0)
        {
            StartCoroutine(aIStateMachine.SetState(aIStateMachine.seekingState));
            return;
        }

        VisibleEntities = newVisibleEntities;
        base.TakeALook();
    }

    public override bool DoesItNeedToMove()
    {
        if (targetTile == null)
            GetTarget();

        Debug.Log("Current speed " + Move.currentSpeed + " " + targetTile.transform.position);
        if (Move.currentSpeed > 0 && !ActionCheck())
            if (Vector2.Distance(targetTile.transform.position, aIStateMachine.transform.position) >= 2 && Move.isNextTileFree(targetTile))
            {
                Debug.Log("Target is " + targetTile.GetComponent<TileContainer>().entityOnTile);
                return true;
            }
            else
                return false;

        return false;
    }

    public override bool ActionCheck()
    {
        Debug.Log("Action check " + VisibleEntities.Count);
        if (aIStateMachine.GetComponent<CharacterStats>().mainActive)
            foreach (KeyValuePair<Entity, BasicTile> entity in VisibleEntities)
                if (Vector2.Distance(entity.Value.transform.position, aIStateMachine.transform.position) < 2 && entity.Key.gameObject.tag != aIStateMachine.gameObject.tag)
                    return true;

        return false;
    }

    public override IEnumerator TryToAct()
    {
        Debug.Log("Shoot");
        yield return aIAbilityController.UseAction("fireBalt_Cantrip", targetTile);
        yield return base.TryToAct();
    }

    protected override void GetTarget()
    {
        TakeALook();

        SortPriorityList();

        List< DataTypeHolderScript.enemyInList > priorityList = aIStateMachine.enemyList;

        Debug.Log("PriorityList:");
        foreach(DataTypeHolderScript.enemyInList enemyInList in priorityList)
        {
            Debug.Log(enemyInList.enemy + " " + enemyInList.enemy.transform.position + " " + enemyInList.priorityRate);
        }

        if (priorityList[0].pathLength != 0 || priorityList[priorityList.Count - 1].pathLength == 0)
        {
            targetTile = map.GetTile(priorityList[0].enemy.transform.position);
            Debug.Log("Target on " + targetTile.transform.position);
        }
        else
        {
            int i = 1;
            while (priorityList[i].pathLength == 0)
                i++;

            targetTile = map.GetTile(priorityList[i].enemy.transform.position);
        }

        Debug.Log(targetTile + " " + targetTile.transform.position);
        base.GetTarget();
    }

    public override IEnumerator MoveToTaget()
    {
        yield return Move.MakeAStepTo(targetTile);
        Debug.Log("STEP DONE");

        yield break;
    }
}
