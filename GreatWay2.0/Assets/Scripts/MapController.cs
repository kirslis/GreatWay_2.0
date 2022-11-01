using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] AntityContainer _antityContainer;
    [SerializeField] GridContainer _gridContainer;
    [SerializeField] Button _endOfTurnButton;

    private bool IsGenerated = false;

    public bool isGenerated { get { return IsGenerated; } }

    public void StartReduct()
    {
        _gridContainer.StartReduct();
    }

    public void AbortReduct()
    {
        _gridContainer.AbortReduct();
    }

    public void ChangeTile(BasicTile tile)
    {
        _gridContainer.ChangeTile(tile);
    }

    public void GenerateMap(int sizeX, int sizeY)
    {
        if (IsGenerated)
            ClearMap();

        _gridContainer.GenerateMap(sizeX, sizeY);
        _antityContainer.GenerateCreatures();
        IsGenerated = true;
    }

    public void ClearMap()
    {
        _gridContainer.DeleteMap();
        _antityContainer.DeleteCreatures();
        IsGenerated = false;
    }

    public void NextTurn()
    {
        _antityContainer.NextTurn();
        StartCoroutine(RotateButtonCoroutine());
    }

    IEnumerator RotateButtonCoroutine()
    {
        _endOfTurnButton.interactable = false;
        for (int i = 0; i < 6; i++)
        {
            _endOfTurnButton.transform.rotation = Quaternion.Euler(0, 0, 60 * (i + 1));
            yield return new WaitForSeconds(0.2f);
        }
        _endOfTurnButton.interactable = true;
    }
}
