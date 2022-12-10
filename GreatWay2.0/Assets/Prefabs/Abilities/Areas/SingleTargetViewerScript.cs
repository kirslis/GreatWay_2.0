using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New SingleTargetViwer", menuName = "Single Target Viwer", order = 51)]
public class SingleTargetViewerScript : BasicUsibleTargetAreaViewer
{
    public override void Awake()
    {
        //base.Awake();
        if (FindObjectOfType<AbilityManager>() != null)
            TargetCursor = FindObjectOfType<AbilityManager>().GetComponentInChildren<PulseScript>();
        Debug.Log(TargetCursor);
    }

    override public void Reaim(BasicTile TargetTile, string TargetTag)
    {

        base.Reaim(TargetTile, TargetTag);


        if (IsTileInTaget(TargetTile) && TargetTile.GetComponent<TileContainer>().entityOnTile != null && TargetTile.GetComponent<TileContainer>().entityOnTile.tag == TargetTag)
        {
            Targets.Clear();
            int i = 0;
            while (TargetTiles[i].Tile != TargetTile)
                i++;

            Targets.Add(new DataTypeHolderScript.TargetAntity(TargetTile.GetComponent<TileContainer>().entityOnTile, TargetTiles[i].TypeOfTarget));
            TargetCursor.transform.position = TargetTile.GetComponent<TileContainer>().entityOnTile.transform.position;
        }
    }
}
