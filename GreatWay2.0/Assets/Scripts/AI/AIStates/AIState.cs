using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public delegate void Action();

    protected AIStateMachine aIStateMachine;
    protected AIAbilityController aIAbilityController;
    protected AIVisionConroller aIVisionConroller;
    protected AIMove Move;
    protected List<KeyValuePair<Entity, BasicTile>> VisibleEntities = new List<KeyValuePair<Entity, BasicTile>>();
    protected GridContainer map;

    public List<KeyValuePair<Entity, BasicTile>> visibleEntities { get { return VisibleEntities; } }

    public virtual void Init(AIStateMachine stateMachine, List<KeyValuePair<Entity, BasicTile>> VisibleEntities)
    {
        map = FindObjectOfType<GridContainer>();
        aIStateMachine = stateMachine;
        aIAbilityController = stateMachine.GetComponent<AIAbilityController>();
        aIVisionConroller = stateMachine.GetComponent<AIVisionConroller>();
        Move = aIStateMachine.GetComponent<AIMove>();
        this.VisibleEntities = VisibleEntities;
    }

    public virtual IEnumerator StateStart()
    {
        Debug.Log("State start");
        yield return null;
    }

    public virtual IEnumerator EndOfTurn()
    {
        yield break;
    }

    public virtual IEnumerator NewTurn()
    {
        yield break;
    }

    public virtual void TakeALook()
    {
    }

    public virtual IEnumerator MoveToTaget()
    {
        Debug.Log("speed " + Move.currentSpeed);
        yield break;
    }

    public virtual IEnumerator TryToAct()
    {
        yield break;
    }

    public virtual IEnumerator End()
    {
        yield break;
    }

    public virtual bool DoesItNeedToMove()
    {
        return true;
    }

    protected bool EnemyCheck(AIVisionConroller aIVisionConroller)
    {
        Debug.Log("ADDING CHECk");
        List<KeyValuePair<Entity, BasicTile>> NewVisibleCreatures = new List<KeyValuePair<Entity, BasicTile>>();
        List<BasicTile> VisibleTiles = aIVisionConroller.visibleTiles;

        foreach (BasicTile tile in VisibleTiles)
            if (tile.GetComponent<TileContainer>().entityOnTile != null)
                NewVisibleCreatures.Add(new KeyValuePair<Entity, BasicTile>(tile.GetComponent<TileContainer>().entityOnTile, tile));

        bool haveEnemies = false;

        foreach (KeyValuePair<Entity, BasicTile> creature in NewVisibleCreatures)
            if (creature.Key.tag != aIVisionConroller.tag)
            {
                aIStateMachine.AddEnemyToList(creature.Key);
                haveEnemies = true;
            }

        return haveEnemies;
    }

    public virtual bool ActionCheck()
    {
        return false;
    }

    protected bool IsTileAvailable(KeyValuePair<Entity, BasicTile> tile)
    {
        return Move.IsTaileAvalible(tile.Value);
    }

    protected void ClearUnAvailableTiles(List<KeyValuePair<Entity, BasicTile>> tiles)
    {
        List<KeyValuePair<Entity, BasicTile>> availableTiles = new List<KeyValuePair<Entity, BasicTile>>();

        foreach (KeyValuePair<Entity, BasicTile> tile in tiles)
            if (IsTileAvailable(tile))
                availableTiles.Add(tile);

        tiles = availableTiles;
    }


}
