using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    [SerializeField] int _speed;

    private List<BasicTile> PatrouleTrail = new List<BasicTile>();
    private GridContainer Grid;
    private AIMoveActions Input;
    private bool IsTrailCreating = false;

    public BasicTile addTilePatrouleTrail { set { } }

    private void Awake()
    {
        Input = new AIMoveActions();

        Input.actions.LMB.performed += context =>
        {
            if (IsTrailCreating)
            {
                BasicTile newTile = Grid.GetChosenTile();
                if (!PatrouleTrail.Contains(newTile))
                    PatrouleTrail.Add(newTile);
            }
        };

        Input.actions.RMB.performed += context =>
        {
            if (IsTrailCreating)
            {
                StopCreatingPatrouleTrial();
            }
        };

        Input.Enable();

        Grid = FindObjectOfType<GridContainer>();
    }

    public void CreatePatrouleTrail()
    {
        IsTrailCreating = true;
        Grid.StartReduct();
    }

    private void StopCreatingPatrouleTrial()
    {
        IsTrailCreating = false;
        Grid.AbortReduct();
    }

    public void MoveToNextPatrolePoint()
    {
        BasicTile currentTile = Grid.GetTile(transform.position);

        if (currentTile == PatrouleTrail[0])
        {
            BasicTile t = PatrouleTrail.First();
            PatrouleTrail.RemoveAt(0);
            PatrouleTrail.Add(t);
        }

        List<BasicTile> Path = FindPath(currentTile, PatrouleTrail[0]);
        foreach (BasicTile t in Path)
        {
            t.ChangeColor(Color.yellow);
        }
    }

    private List<BasicTile> FindPath(BasicTile startTile, BasicTile endTile)
    {
        List<BasicTile> reachableTiles = new List<BasicTile> { startTile };
        List<BasicTile> exploredTiles = new List<BasicTile>();

        while (reachableTiles.Count > 0)
        {
            // Choose some node we know how to reach.
            BasicTile tile = chooseNextTile(reachableTiles, endTile);

            //# If we just got to the goal node, build and return the path.
            if (tile == endTile)
                return BuildPath(endTile);

            //# Don't repeat ourselves.
            reachableTiles.Remove(tile);
            exploredTiles.Add(tile);

            //# Where can we get from here that we haven't explored before?
            List<BasicTile> newReachebleTiles = Grid.getAdjacentTiles(tile);
            foreach (BasicTile newTile in newReachebleTiles)
                if (exploredTiles.Contains(newTile))
                    newReachebleTiles.Remove(newTile);

            foreach (BasicTile newTile in newReachebleTiles)
            {
                // First time we see this node?
                if (newTile.isPasseble && !reachableTiles.Contains(newTile))
                    reachableTiles.Add(newTile);

                // If this is a new path, or a shorter path than what we have, keep it.
                if (tile.SpentMoveSpeed + newTile.currentPathCost < newTile.SpentMoveSpeed)
                {
                    newTile.previosTile = tile;
                    newTile.SpentMoveSpeed = tile.SpentMoveSpeed + newTile.currentPathCost;
                }
            }
        }
        //# If we get here, no path was found :(
        return null;
    }

    private List<BasicTile> BuildPath(BasicTile endTile)
    {
        List<BasicTile> path = new List<BasicTile>();
        while (endTile != null)
        {
            path.Add(endTile);
            endTile = endTile.previosTile;
        }
        return path;
    }

    private BasicTile chooseNextTile(List<BasicTile> reachebleTiles, BasicTile finalNode)
    {
        int minCost = 1000000000; //infinity
        BasicTile bestTile = null;

        foreach (BasicTile tile in reachebleTiles)
        {
            int startCost = tile.SpentMoveSpeed;
            int costToFinalNode = (int)Vector2.Distance(tile.transform.position, finalNode.transform.position);
            int totalCost = startCost + costToFinalNode;

            if (minCost > totalCost)
            {
                minCost = totalCost;
                bestTile = tile;
            }
        }
        return bestTile;
    }
}
