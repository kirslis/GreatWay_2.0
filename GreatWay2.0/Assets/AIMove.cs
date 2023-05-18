using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMove : Move
{
    private List<BasicTile> PatroleTrail = new List<BasicTile>();
    private int CurrentPatrolePointIndex = 0;

    private List<BasicTile> reachableTiles = new List<BasicTile>();
    private List<BasicTile> exploredTiles = new List<BasicTile>();

    public bool isPatroling { get { return PatroleTrail.Count > 0; } }

    public List<BasicTile> patroleTrail
    {
        set
        {
            PatroleTrail = value;

            Debug.Log("Trail set " + PatroleTrail.Count);

            List<BasicTile> goodPatrolePoints = new List<BasicTile>();

            foreach (BasicTile tile in PatroleTrail)
                if (FindShortPath(tile).Count > 0)
                    goodPatrolePoints.Add(tile);

            PatroleTrail = goodPatrolePoints;
            

        }
    }

    public BasicTile GetNextIdlingTile()
    {
        if (isPatroling)
        {
            if (GridContainer.GetTile(transform.position) == PatroleTrail[CurrentPatrolePointIndex])
            {
                CurrentPatrolePointIndex++;
                if (CurrentPatrolePointIndex == PatroleTrail.Count)
                    CurrentPatrolePointIndex = 0;
            }

            Debug.Log("Next patrole point - " + PatroleTrail[CurrentPatrolePointIndex]);
            Debug.Log(exploredTiles.Count);
            Debug.Log(reachableTiles.Count);
            List<BasicTile> path = FindGoodPath(PatroleTrail[CurrentPatrolePointIndex]);
            return path[path.Count - 2];
        }
        else return null;
    }

    public int GetShortestPathLength(BasicTile targetTile)
    {
        List<BasicTile> path = FindShortPath(targetTile);
        if (path != null)
            return path.Count;
        
        return 0;
    }

    public void MoveToNextPatrolePoint()
    {
        StartCoroutine(MoveToNextPoinCourutine());
    }

    public bool IsTaileAvalible(BasicTile tile)
    {
        List<BasicTile> adjactedTiles = GridContainer.getAllAdjacentTiles(tile);

        foreach (BasicTile adjactedTile in adjactedTiles)
            if (adjactedTile.isPasseble)
                return true;
        
        return false;
    }

    public IEnumerator MakeAPatroleStep()
    {
        BasicTile currentTile = GridContainer.GetTile(transform.position);

        if (currentTile == PatroleTrail[CurrentPatrolePointIndex])
        {
            CurrentPatrolePointIndex++;
            if (CurrentPatrolePointIndex == PatroleTrail.Count)
                CurrentPatrolePointIndex = 0;
        }

        List<BasicTile> Path = FindGoodPath(PatroleTrail[CurrentPatrolePointIndex]);

        if (Path[Path.Count - 2].isPasseble)
        {
            CurrentSpeed -= Path[Path.Count - 2].currentPathCost;
            yield return MoveToPointCourutine(Path[Path.Count - 2]);
        }
    }

    private List<BasicTile> FindGoodPath(BasicTile targetTile)
    {

        List<BasicTile> LongPath = null;
        if(targetTile.isPasseble)
            LongPath = FindFreePath(targetTile);
        List<BasicTile> ShortPath = FindShortPath(targetTile);

        if (LongPath == null || LongPath.Count - ShortPath.Count > MaxSpeed * 4)
            return ShortPath;

        return LongPath;
    }

    IEnumerator MoveToNextPoinCourutine()
    {
        while (currentSpeed > 0)
        {
            yield return MakeAPatroleStep();
        }

    }

    private List<BasicTile> FindFreePath(BasicTile endTile)
    {
        ResetCost(reachableTiles);
        ResetCost(exploredTiles);

        BasicTile startTile = GridContainer.GetTile(transform.position);
        reachableTiles.Add(startTile);

        while (reachableTiles.Count > 0)
        {
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

    private List<BasicTile> FindShortPath(BasicTile endTile)
    {
        ResetCost(reachableTiles);
        ResetCost(exploredTiles);

        BasicTile startTile = GridContainer.GetTile(transform.position);
        reachableTiles.Add(startTile);

        while (reachableTiles.Count > 0)
        {
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
                    if ((newTile.isPasseble || newTile.GetComponent<TileContainer>().entityOnTile != null) && !reachableTiles.Contains(newTile))
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

        Tiles.Clear();
    }

    private List<BasicTile> BuildPath(BasicTile endTile)
    {
        List<BasicTile> path = new List<BasicTile>();
        while (endTile != null)
        {
            endTile.ChangeColor(Color.red);
            path.Add(endTile);
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

    public bool isNextTileFree(BasicTile targetTile)
    {
        targetTile.isPasseble = true;
        List<BasicTile> Path = FindGoodPath(targetTile);
        targetTile.isPasseble = false;

        return Path[Path.Count - 2].isPasseble;
    }

    public IEnumerator MakeAStepTo(BasicTile targetTile)
    {
        Debug.Log("Make a step to" + targetTile.GetComponent<TileContainer>().entityOnTile);

        bool baseIsPasseble = targetTile.isPasseble;
        targetTile.isPasseble = true;
        List<BasicTile> Path = FindGoodPath(targetTile);
        targetTile.isPasseble = baseIsPasseble;

        if (Path[Path.Count - 2].isPasseble)
        {
            Debug.Log("To " + Path[Path.Count - 1].transform.position);
            CurrentSpeed -= Path[Path.Count - 2].currentPathCost;
            yield return MoveToPointCourutine(Path[Path.Count - 2]);
        }

    }
}
