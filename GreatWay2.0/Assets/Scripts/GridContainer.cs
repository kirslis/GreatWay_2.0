using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridContainer : MonoBehaviour
{
    [SerializeField] BasicTile _tile;
    [SerializeField] GameObject ContainerObject;
    [SerializeField] GameObject _redactorCursor;
    [SerializeField] GlobalVisionController _globalVisionController;

    private int SizeX;
    private int SizeY;
    private bool IsReducting;
    private GameObject RedactorCursor;

    private List<List<BasicTile>> Container = new List<List<BasicTile>>();
    private List<GameObject> Colls = new List<GameObject>();

    private List<BasicTile> PassebleTiles = new List<BasicTile>();
    private List<BasicTile> PassedTiles = new List<BasicTile>();
    private int CountOfPassedTiles = 0;

    public List<BasicTile> passebleTiles { get { return PassebleTiles; } }
    public List<BasicTile> passedTiles { get { return PassedTiles; } }

    public int countOfPassedTiles { get { return CountOfPassedTiles; } set { CountOfPassedTiles = value; } }
    public int sizeX { get { return SizeX; } }
    public int sizeY { get { return SizeY; } }
    public float yStep { get { return 1.0f / SizeY; } }
    public List<List<BasicTile>> container { get { return Container; } }
    public Vector2 redactorCursorPos { get { return RedactorCursor.transform.position; } }

    public BasicTile GetTile(Vector2 Pos)
    {
        return Container[(int)Pos.x][(int)Pos.y];
    }

    public void StartReduct()
    {
        IsReducting = true;
        RedactorCursor.SetActive(true);
    }

    public void AbortReduct()
    {
        IsReducting = false;
        RedactorCursor.SetActive(false);
    }

    public bool IsChosenTileFree()
    {
        Vector2 pos = RedactorCursor.transform.position;
        return Container[(int)pos.x][(int)pos.y].isPasseble;
    }

    public BasicTile GetChosenTile()
    {
        Vector2 pos = RedactorCursor.transform.position;
        return Container[(int)pos.x][(int)pos.y];
    }

    public bool TryChangeTile(BasicTile tile)
    {
        Vector2 Pos = RedactorCursor.transform.position;
        if (!Container[(int)Pos.x][(int)Pos.y].GetComponent<TileContainer>().IsContainEntity() || tile.isPasseble)
        {
            if (Container[(int)Pos.x][(int)Pos.y] != null && Container[(int)Pos.x][(int)Pos.y].name != tile.name)
            {
                Destroy(Container[(int)Pos.x][(int)Pos.y].gameObject);
                Container[(int)Pos.x][(int)Pos.y] = Instantiate(tile, Colls[(int)Pos.x].transform);
                Container[(int)Pos.x][(int)Pos.y].name = tile.name;
                Container[(int)Pos.x][(int)Pos.y].transform.position = new Vector3(Pos.x, Pos.y, 1);
                _globalVisionController.AddToInvisibleInGame(Container[(int)Pos.x][(int)Pos.y]);
            }
            return true;
        }

        return false;
    }

    private void MakePosValid(ref Vector2 pos)
    {
        if (pos.x < 0)
            pos.x = 0;
        else if (pos.x > SizeX)
            pos.x = SizeX - 1;
        if (pos.y < 0)
            pos.y = 0;
        else if (pos.y > SizeY)
            pos.y = SizeY - 1;
    }

    private void Awake()
    {
        RedactorCursor = Instantiate(_redactorCursor, transform);
        RedactorCursor.SetActive(false);
    }

    private void Update()
    {
        if (IsReducting)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            MakePosValid(ref pos);

            RedactorCursor.transform.position = new Vector3((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), -3);
        }
    }

    public void GenerateMap(int xSize, int ySize)
    {
        SizeX = xSize;
        SizeY = ySize;

        GameObject BaseCollFolder = new GameObject();
        for (int i = 0; i < SizeX; i++)
        {
            GameObject NewColl = Instantiate(BaseCollFolder, ContainerObject.transform);
            NewColl.name = "coll" + i.ToString();
            Container.Add(new List<BasicTile>());
            Colls.Add(NewColl);

            for (int j = 0; j < SizeY; j++)
            {
                BasicTile NewTile = Instantiate(_tile, NewColl.transform);
                NewTile.transform.position = new Vector3(i, j, 1);
                NewTile.name = _tile.name;
                Container[i].Add(NewTile);
            }
        }
        Destroy(BaseCollFolder);
    }

    public void DeleteMap()
    {
        int i = 0;
        foreach (List<BasicTile> Coll in Container)
        {
            foreach (BasicTile Tile in Coll)
                Destroy(Tile.gameObject);

            Destroy(Colls[i].gameObject);
            i++;
        }

        Colls.Clear();
        Container.Clear();
    }

    private void CheckCell(Vector2 StartPos, Vector2 TargetPos, int Speed)
    {
        if (TargetPos.x >= 0 && TargetPos.y >= 0 && TargetPos.x < SizeX && TargetPos.y < SizeY)
        {
            BasicTile StartTile = Container[(int)StartPos.x][(int)StartPos.y];
            BasicTile TargetTile = Container[(int)TargetPos.x][(int)TargetPos.y];

            if (TargetTile.isSeen && TargetTile.isPasseble && Speed >= TargetTile.currentPathCost && (TargetTile.SpentMoveSpeed == -1 || TargetTile.SpentMoveSpeed > StartTile.SpentMoveSpeed + TargetTile.currentPathCost))
            {
                if (TargetTile.visibleColor != Color.red)
                    TargetTile.ChangeColor(Color.cyan);
                TargetTile.SpentMoveSpeed = StartTile.SpentMoveSpeed + TargetTile.currentPathCost;
                PassebleTiles.Add(TargetTile);
                LightUpWais(new Vector3(TargetPos.x, TargetPos.y), Speed - TargetTile.currentPathCost);
            }
        }
    }

    public void LightUpWaisWrapped(Vector3 Pos, int MoveSpeed)
    {
        ResetLightedTiles();
        GetTile(Pos).SpentMoveSpeed = 0;
        LightUpWais(Pos, MoveSpeed);
    }

    private void LightUpWais(Vector3 Pos, int MoveSpeed)
    {
        CheckCell(Pos, new Vector2(Pos.x + 1, Pos.y), MoveSpeed);
        CheckCell(Pos, new Vector2(Pos.x - 1, Pos.y), MoveSpeed);
        CheckCell(Pos, new Vector2(Pos.x, Pos.y + 1), MoveSpeed);
        CheckCell(Pos, new Vector2(Pos.x, Pos.y - 1), MoveSpeed);
    }

    public void ResetLightedTiles()
    {
        foreach (BasicTile tile in PassebleTiles)
        {
            if (tile.visibleColor != Color.red)
                tile.RefreshTile();
        }

        PassebleTiles.Clear();
    }

    public bool IsCellpasseble(PlayerMove player, Vector2 Pos)
    {
        return Pos.x >= 0 && Pos.x < Container.Count
            && Pos.y >= 0 && Pos.y < Container[0].Count &&
            Container[(int)Pos.x][(int)Pos.y].isSeen &&
            (PassebleTiles.Contains(Container[(int)Pos.x][(int)Pos.y]) &&
            player.currentSpeed >= Container[(int)Pos.x][(int)Pos.y].currentPathCost || (Container[(int)Pos.x][(int)Pos.y] == PassedTiles[CountOfPassedTiles - 2]));
    }

    public void AddCellToPassed(PlayerMove player, Vector2 Pos)
    {

        if (CountOfPassedTiles == 0 && (int)Mathf.Abs(Pos.x - player.transform.position.x) + (int)Mathf.Abs(Pos.y - player.transform.position.y) < 2)
        {
            Container[(int)player.transform.position.x][(int)player.transform.position.y].ChangeColor(Color.red);
            AddTileAsPassed(Container[(int)player.transform.position.x][(int)player.transform.position.y]);
        }

        if (CountOfPassedTiles != 0 && (int)Mathf.Abs(Pos.x - passedTiles[CountOfPassedTiles - 1].transform.position.x) + (int)Mathf.Abs(Pos.y - passedTiles[CountOfPassedTiles - 1].transform.position.y) < 2)
        {
            if (CountOfPassedTiles > 1 && PassedTiles[CountOfPassedTiles - 2] == Container[(int)Pos.x][(int)Pos.y])
                DeleteCellsFromPassed(player, PassedTiles[CountOfPassedTiles - 1].transform.position);
            else
            {
                player.currentSpeed -= Container[(int)Pos.x][(int)Pos.y].currentPathCost;
                Container[(int)Pos.x][(int)Pos.y].ChangeColor(Color.red);
                AddTileAsPassed(Container[(int)Pos.x][(int)Pos.y]);
            }


            LightUpWaisWrapped(Pos, player.currentSpeed);
        }

        if (CountOfPassedTiles == 1 && Pos.Equals(player.transform.position))
        {
            Container[(int)player.transform.position.x][(int)player.transform.position.y].ChangeColor(Color.cyan);
            CountOfPassedTiles--;
        }

    }

    private void AddTileAsPassed(BasicTile tile)
    {
        CountOfPassedTiles++;

        if (PassedTiles.Count >= CountOfPassedTiles)
            PassedTiles[CountOfPassedTiles - 1] = tile;
        else
            PassedTiles.Add(tile);
    }

    public void DeleteCellsFromPassed(PlayerMove player, Vector2 Pos)
    {
        player.currentSpeed += Container[(int)Pos.x][(int)Pos.y].currentPathCost;
        CountOfPassedTiles--;
        if (!IsTilePassed(Container[(int)Pos.x][(int)Pos.y]))
            Container[(int)Pos.x][(int)Pos.y].RefreshTile();
    }

    public bool IsTilePassed(BasicTile tile)
    {
        for (int i = 0; i < CountOfPassedTiles; i++)
        {
            if (PassedTiles[i] == tile)
                return true;
        }

        return false;
    }

    public void ResetPath()
    {
        foreach (BasicTile tile in PassedTiles)
            tile.RefreshTile();

        PassebleTiles.Clear();
        CountOfPassedTiles = 0;
    }

    public void MakeTilesInVisible(List<BasicTile> tiles)
    {
        foreach (BasicTile tile in tiles)
            if (tile != null)
                tile.isVisible = false;
    }

    public void MakeTilesVisible(List<BasicTile> tiles)
    {
        foreach (BasicTile tile in tiles)
            tile.isVisible = true;
    }
}
