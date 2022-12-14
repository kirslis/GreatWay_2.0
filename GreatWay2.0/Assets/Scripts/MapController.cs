using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] AntityContainer _antityContainer;
    [SerializeField] GridContainer _gridContainer;
    [SerializeField] Button _endOfTurnButton;
    [SerializeField] GameInterface _gameInterface;
    [SerializeField] AbilityManager _abilityManger;

    private bool IsGenerated = false;

    public bool isInterfaceActive { set { _gameInterface.isEnterfaceActive = value; } }
    public bool isGenerated { get { return IsGenerated; } }

    public void StartReduct()
    {
        _gridContainer.StartReduct();
    }

    public void AbortReduct()
    {
        _gridContainer.AbortReduct();
    }

    public bool TryChangeTile(BasicTile tile)
    {
        return _gridContainer.TryChangeTile(tile);
    }

    public bool TryAddCreature(Antity creature, Vector2 Pos)
    {
        if (_gridContainer.IsChosenTileFree())
        {
            _antityContainer.AddCreature(creature, _gridContainer.redactorCursorPos);
            return true;
        }

        return false;
    }

    public bool TryAddEnviroment(Enviroment obj, Vector2 Pos, float zAngle)
    {
        _gridContainer.MakePosValid(ref Pos);

        Debug.Log(_gridContainer.GetTile(Pos).GetComponent<TileContainer>().objectOnTile != null);
        if (_gridContainer.IsChosenTileFree() || _gridContainer.GetTile(Pos).GetComponent<TileContainer>().objectOnTile != null)
        {
            _antityContainer.AddEnviroment(obj, _gridContainer.redactorCursorPos, zAngle);
            return true;
        }

        return false;
    }

    public void GenerateMap(int sizeX, int sizeY)
    {
        StartCoroutine(GenerateMapCourutine(sizeX, sizeY));
    }

    IEnumerator GenerateMapCourutine(int sizeX, int sizeY)
    {
        isInterfaceActive = false;
        if (IsGenerated)
            ClearMap();

        _gridContainer.GenerateMap(sizeX, sizeY);
        yield return null;

        _antityContainer.GenerateCreatures();
        IsGenerated = true;
    }

    public void ClearMap()
    {
        _gridContainer.DeleteMap();
        _antityContainer.DeleteCreatures();
        IsGenerated = false;
        Debug.Log("DELETEMAP");
    }

    public void NextTurn()
    {
        _abilityManger.curentAbility = null;
        _antityContainer.NextTurn();
        StartCoroutine(RotateButtonCoroutine());
    }

    IEnumerator RotateButtonCoroutine()
    {
        isInterfaceActive = false;
        _endOfTurnButton.interactable = false;
        for (int i = 0; i < 6; i++)
        {
            _endOfTurnButton.transform.rotation = Quaternion.Euler(0, 0, 60 * (i + 1));
            yield return new WaitForSeconds(0.4f);
        }
        _endOfTurnButton.interactable = true;
        isInterfaceActive = true;
    }
}
