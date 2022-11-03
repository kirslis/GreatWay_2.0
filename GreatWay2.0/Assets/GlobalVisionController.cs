using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVisionController : MonoBehaviour
{
    [SerializeField] GridContainer _gridContainer;
    [SerializeField] AntityContainer _antityContainer;

    private List<BasicTile> VisibleTiles = new List<BasicTile>();

    public void AllLookOut()
    {
        _gridContainer.MakeTilesInVisible(VisibleTiles);
        VisibleTiles.Clear();

        List<Antity> antities = _antityContainer.antityes;

        foreach (Antity antity in antities)
            antity.GetComponent<VisionController>().LookOut();

        Debug.Log("ERLEY");
        _gridContainer.MakeTilesVisible(VisibleTiles);
    }

    public void AddVisibleTiles(List<BasicTile> newVisibleTiles)
    {
        foreach (BasicTile newTile in newVisibleTiles)
            VisibleTiles.Add(newTile);
    }
}
