using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUsibleTargetAreaViewer : ScriptableObject
{
    [SerializeField] public string _typeTag;

    protected PulseScript TargetCursor;
    protected GridContainer Grid;
    protected List<TargetTile> TargetTiles = new List<TargetTile>();
    protected float YDeviation = 0.2f;
    protected bool IsAiming;
    protected List<Antity> Targets = new List<Antity>();

    public List<Antity> targets { get { return Targets; } }

    // 0 - normal target
    // 1 - obstacle target
    // 2 - unTargetble

    protected struct TargetTile
    {
        public BasicTile Tile;
        public int TypeOfTarget;

        public TargetTile(BasicTile tile, int typeOfTile)
        {
            Tile = tile;
            TypeOfTarget = typeOfTile;
        }
    }

    virtual public void Awake()
    {
    }

    virtual public void Reaim(BasicTile TagetTile, string TargetTag)
    {
    }

    virtual public void LightUpTargetArea(Vector3 StartPos, int Range, string tag)
    {
        TargetCursor.gameObject.SetActive(true);

        IsAiming = true;
        Grid = FindObjectOfType<GridContainer>();

        Debug.Log(StartPos + " " + Range + " " + tag + " " + Grid);
        TargetTiles.Clear();

        int leftBorder = StartPos.x - Range > 0 ? (int)StartPos.x - Range : 0;
        int rightBorder = StartPos.x + Range < Grid.sizeX ? (int)StartPos.x + Range : Grid.sizeX - 1;

        int bottomBorder = StartPos.y - Range > 0 ? (int)StartPos.y - Range : 0;
        int topBorder = StartPos.y + Range < Grid.sizeY ? (int)StartPos.y + Range : Grid.sizeY - 1;

        for (int i = bottomBorder; i <= topBorder; i++)
            for (int j = leftBorder; j <= rightBorder; j++)
                if (Mathf.Sqrt(Mathf.Pow(j - StartPos.x, 2) + Mathf.Pow(i - StartPos.y, 2)) <= Range)
                    CheckTile(StartPos, Grid.GetTile(new Vector2(j, i)), tag);
    }

    private void ChangeTargetTileColor(BasicTile tile, Color NewColor, string tag)
    {
        tile.ChangeColor(NewColor);
        tile.GetComponent<TileContainer>().MarkTargets(NewColor, tag);
    }

    private void RefreshTargetTileColor(BasicTile tile)
    {
        tile.RefreshTile();
        tile.GetComponent<TileContainer>().RefreshTargets();
    }

    private void CheckTile(Vector3 StartPos, BasicTile tile, string tag)
    {
        float Distance = Vector2.Distance(tile.transform.position, StartPos);
        Vector2 startPos = new Vector2(StartPos.x, StartPos.y + YDeviation);

        Vector2 direction = new Vector2(tile.transform.position.x, tile.transform.position.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, direction - startPos, Distance, LayerMask.GetMask("Grid"));
        int i = 0;
        bool isAttackeble = true;
        bool isVisible = true;

        while (i < hits.Length && isAttackeble)
        {
            if (!hits[i].collider.GetComponent<BasicTile>().isAttackThrought)
                isAttackeble = false;
            else if (!hits[i].collider.GetComponent<BasicTile>().isVisible || !hits[i].collider.GetComponent<BasicTile>().isSeeThrought)
                isVisible = false;
            i++;
        }

        if (isAttackeble && isVisible)
        {
            ChangeTargetTileColor(tile, Color.green, tag);

            TargetTiles.Add(new TargetTile(tile, 0));

            Debug.DrawRay(startPos, (Vector2)tile.transform.position - startPos, Color.green, 3);
            return;
        }

        if (isAttackeble && !isVisible)
        {
            ChangeTargetTileColor(tile, Color.yellow, tag);

            TargetTiles.Add(new TargetTile(tile, 1));

            Debug.DrawRay(startPos, (Vector2)tile.transform.position - startPos, Color.yellow, 3);
            return;
        }
        ChangeTargetTileColor(tile, Color.red, tag);

        TargetTiles.Add(new TargetTile(tile, 2));

        Debug.DrawRay(startPos, (Vector2)tile.transform.position - startPos, Color.red, 3);
    }

    virtual public void AbortAiming()
    {
        TargetCursor.gameObject.SetActive(false);

        foreach (TargetTile tile in TargetTiles)
            RefreshTargetTileColor(tile.Tile);
        TargetTiles.Clear();
        IsAiming = false;
    }

    public bool IsTileInTaget(BasicTile tile)
    {
        foreach (TargetTile targetTile in TargetTiles)
            if (targetTile.Tile == tile)
                return true;

        return false;
    }
}
