using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGeneralWonderingState : AIGeneralIdlingState
{
    private int RoundsToSeak;
    private const int MaxCountOfSeakRounds = 5;
    private BasicTile PreviosTile = null;

    public override void Init(AIStateMachine stateMachine, List<KeyValuePair<Entity, BasicTile>> VisibleEntities)
    {
        base.Init(stateMachine, VisibleEntities);
        RoundsToSeak = MaxCountOfSeakRounds;
    }

    public override IEnumerator NewTurn()
    {
        RoundsToSeak--;
        if (RoundsToSeak == 0)
            yield return aIStateMachine.SetState(aIStateMachine.idleState);

       base.NewTurn();
    }

    public override bool DoesItNeedToMove()
    {
        return Move.currentSpeed > 0 && HaveWayToGo();
    }

    private bool HaveWayToGo()
    {
        List<BasicTile> adjacentTiles = map.getAdjacentTiles(map.GetTile(aIStateMachine.transform.position));

        foreach (BasicTile tile in adjacentTiles)
            if (tile.isPasseble)
                return true;

        return false;
    }

    public override IEnumerator MoveToTaget()
    {
        List<BasicTile> adjacentTiles = map.getAdjacentTiles(map.GetTile(aIStateMachine.transform.position));
        int randVal = Random.Range(1, 101);

        if (PreviosTile == null)
            PreviosTile = adjacentTiles[0];
        else
            Debug.Log(randVal + " Move from " + PreviosTile.transform.position);



        Vector2 delta = aIStateMachine.transform.position - PreviosTile.transform.position;
        BasicTile forwardTile = null;

        int i = 0;
        while (i < adjacentTiles.Count && forwardTile == null)
        {
            if ((Vector2)(adjacentTiles[i].transform.position - aIAbilityController.transform.position) == delta)
            {
                Debug.Log("Move Have Next");
                forwardTile = adjacentTiles[i];
                adjacentTiles.RemoveAt(i);
            }
            else
                i++;
        }
        Debug.Log(adjacentTiles.Count);

        i = 0;
        while (i < adjacentTiles.Count && adjacentTiles[i] != PreviosTile)
            i++;

        if (i == adjacentTiles.Count)
            Debug.Log("Move problem");
        else
            adjacentTiles.RemoveAt(i);

        if (randVal < 80 &&
            forwardTile != null && forwardTile.isPasseble)
        {
            Debug.Log("Move forward");
            PreviosTile = map.GetTile(aIStateMachine.transform.position);
            yield return Move.MakeAStepTo(forwardTile);
            yield break;
        }

        adjacentTiles.Remove(PreviosTile);

        if (adjacentTiles.Count > 0)
        {
            Debug.Log("Move 1/2 " + adjacentTiles.Count);
            if (adjacentTiles.Count == 2)
            {
                if (randVal % 2 == 1 && adjacentTiles[1].isPasseble)
                {
                    Debug.Log("Move 1");
                    PreviosTile = map.GetTile(aIStateMachine.transform.position);
                    yield return Move.MakeAStepTo(adjacentTiles[1]);
                    yield break;
                }
            }

            if (adjacentTiles[0].isPasseble)
            {
                PreviosTile = map.GetTile(aIStateMachine.transform.position);
                Debug.Log(adjacentTiles.Count + " Move 2");
                yield return Move.MakeAStepTo(adjacentTiles[0]);
                yield break;
            }
        }

        BasicTile nextTile = PreviosTile;
        PreviosTile = map.GetTile(aIStateMachine.transform.position);
        Debug.Log("Move Back");
        yield return Move.MakeAStepTo(nextTile);
    }
}
