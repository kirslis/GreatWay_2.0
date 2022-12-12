using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Self Target Viewer", menuName = "Self Target Viewer", order = 51)]
public class SelfTargetAreaViewer : BasicUsibleTargetAreaViewer
{
    public override void Awake()
    {
        if (FindObjectOfType<AbilityManager>() != null)
            TargetCursor = FindObjectOfType<AbilityManager>().GetComponentInChildren<PulseScript>();

        base.Awake();
    }
    override public void LightUpTargetArea(Vector3 StartPos, int Range, string tag)
    {
        TargetCursor.gameObject.SetActive(true);

        IsAiming = true;
        Grid = FindObjectOfType<GridContainer>();

        CheckTile(StartPos, Grid.GetTile(StartPos), tag);

        Reaim(TargetTiles[0].Tile, tag);
    }
    
    public override void Reaim(BasicTile TargetTile, string TargetTag)
    {

        if (IsTileInTaget(TargetTile) && TargetTile.GetComponent<TileContainer>().entityOnTile != null && TargetTile.GetComponent<TileContainer>().entityOnTile.tag == TargetTag)
        {
            Debug.Log("REAIM");
            Targets.Clear();
            int i = 0;
            while (TargetTiles[i].Tile != TargetTile)
                i++;

            Targets.Add(new DataTypeHolderScript.TargetAntity(TargetTile.GetComponent<TileContainer>().entityOnTile, TargetTiles[i].TypeOfTarget));
            TargetCursor.transform.position = TargetTile.GetComponent<TileContainer>().entityOnTile.transform.position;
        }
        base.Reaim(TargetTile, TargetTag);

    }
}
