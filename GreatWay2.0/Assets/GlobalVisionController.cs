using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVisionController : MonoBehaviour
{
    [SerializeField] GridContainer _gridContainer;
    [SerializeField] AntityContainer _antityContainer;

    private List<BasicTile> InvisibleInGameTiles = new List<BasicTile>();
    private List<BasicTile> VisibleTiles = new List<BasicTile>();
    private bool IsInReductMode = false;

    public bool isInReductMode { get { return IsInReductMode; } }

    public void AllLookOut()
    {
        _gridContainer.MakeTilesInVisible(VisibleTiles);
        VisibleTiles.Clear();

        List<Antity> antities = _antityContainer.antityes;

        foreach (Antity antity in antities)
            antity.GetComponent<VisionController>().LookOut();

        _gridContainer.MakeTilesVisible(VisibleTiles);
    }

    public void AddVisibleTiles(List<BasicTile> newVisibleTiles)
    {
        foreach (BasicTile newTile in newVisibleTiles)
            VisibleTiles.Add(newTile);
    }

    public void AddToInvisibleInGame(BasicTile tile)
    {
        InvisibleInGameTiles.Add(tile);
    }

    public void EnterEditMode()
    {
        List<List<BasicTile>> Tiles = _gridContainer.container;
        IsInReductMode = true;

        foreach (List<BasicTile> coll in Tiles)
            foreach (BasicTile tile in coll)
            {
                if (!tile.isSeen)
                    InvisibleInGameTiles.Add(tile);

                tile.isVisible = true;
            }
    }

    public void ExitEditMode()
    {
        IsInReductMode = false;
        List<List<BasicTile>> Tiles = _gridContainer.container;

        foreach (BasicTile tile in InvisibleInGameTiles)
            if (tile != null)
                tile.isSeen = false;

        foreach (List<BasicTile> coll in Tiles)
            foreach (BasicTile tile in coll)
            {
                if (tile != null && tile.isSeen)
                    tile.isVisible = false;
            }

        InvisibleInGameTiles.Clear();
        AllLookOut();
    }
}
