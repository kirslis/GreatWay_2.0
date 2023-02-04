using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIMove : Move
{
    private List<BasicTile> PatroleTrail = new List<BasicTile>();
    private int CurrentpatrolePointIndex = 0;

    private List<BasicTile> reachableTiles = new List<BasicTile>();
    private List<BasicTile> exploredTiles = new List<BasicTile>();

    public List<BasicTile> patroleTrail
    {
        set
        {
            PatroleTrail = value;

            Debug.Log("Trail set " + PatroleTrail.Count);
        }
    }

    public void MoveToNextPatrolePoint()
    {
        StartCoroutine(MoveToNextPoinCourutine());
    }

    IEnumerator MoveToNextPoinCourutine()
    {
        while (currentSpeed > 0)
        {
            BasicTile currentTile = GridContainer.GetTile(transform.position);

            if (currentTile == PatroleTrail[CurrentpatrolePointIndex])
            {
                CurrentpatrolePointIndex++;
                if (CurrentpatrolePointIndex == PatroleTrail.Count)
                    CurrentpatrolePointIndex = 0;
            }

            List<BasicTile> Path = FindPath(currentTile, PatroleTrail[CurrentpatrolePointIndex]);

            ResetCost(reachableTiles);
            reachableTiles.Clear();
            ResetCost(exploredTiles);
            exploredTiles.Clear();

            CurrentSpeed -= Path[Path.Count - 2].currentPathCost;
            yield return MoveToPointCourutine(Path[Path.Count - 2]);
        }

    }

    private List<BasicTile> FindPath(BasicTile startTile, BasicTile endTile)
    {

        reachableTiles.Add(startTile);

        while (reachableTiles.Count > 0)
        {
            Debug.Log("CHECK");
            // Choose some node we know how to reach.
            BasicTile tile = chooseNextTile(reachableTiles, endTile);

            //# If we just got to the goal node, build and return the path.
            if (tile == endTile)
            {
                return BuildPath(endTile);
            }

            //# Don't repeat ourselves.
            reachableTiles.Remove(tile);
            exploredTiles.Add(tile);

            //# Where can we get from here that we haven't explored before?
            List<BasicTile> newReachebleTiles = GridContainer.getAdjacentTiles(tile);
            //foreach (BasicTile newTile in newReachebleTiles)
            //    if (exploredTiles.Contains(newTile))
            //        newReachebleTiles.Remove(newTile);

            foreach (BasicTile newTile in newReachebleTiles)
            {
                if (!exploredTiles.Contains(newTile))
                {
                    // First time we see this node?
                    if (newTile.isPasseble && !reachableTiles.Contains(newTile))
                    {
                        reachableTiles.Add(newTile);
                        newTile.AISpentMoveSpeed = tile.AISpentMoveSpeed + newTile.currentPathCost;
                        newTile.previosTile = tile;
                    }
                }
            }
        }
        //# If we get here, no path was found :(
        return null;
    }

    private void ResetCost(List<BasicTile> Tiles)
    {
        foreach (BasicTile tile in Tiles)
            tile.RefreshAISPentMoveSpeed();
    }

    private List<BasicTile> BuildPath(BasicTile endTile)
    {
        List<BasicTile> path = new List<BasicTile>();
        while (endTile != null)
        {
            endTile.ChangeColor(Color.red);
            path.Add(endTile);
            Debug.Log("previosTile = " + endTile.previosTile);
            endTile = endTile.previosTile;
            path.Last().RefreshAISPentMoveSpeed();
        }

        return path;
    }

    private BasicTile chooseNextTile(List<BasicTile> reachebleTiles, BasicTile finalNode)
    {
        int minCost = 1000000000; //infinity
        BasicTile bestTile = null;

        foreach (BasicTile tile in reachebleTiles)
        {
            int startCost = tile.AISpentMoveSpeed;
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
