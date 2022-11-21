using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New SingleTargetViwer", menuName = "Single Target Viwer", order = 51)]
public class SingleTargetViewerScript : BasicUsibleTargetAreaViewer
{
    public override void Awake()
    {
        base.Awake();
        TargetCursor = FindObjectOfType<AbilityManager>().GetComponentInChildren<PulseScript>();
        Debug.Log(TargetCursor);
    }

    override public void Reaim(BasicTile TargetTile, string TargetTag)
    {
        base.Reaim(TargetTile, TargetTag);

        
        if (IsTileInTaget(TargetTile) && TargetTile.GetComponent<TileContainer>().entityOnTile != null && TargetTile.GetComponent<TileContainer>().entityOnTile.tag == TargetTag)
        {
            Targets.Clear();
            Targets.Add(TargetTile.GetComponent<TileContainer>().entityOnTile);
            TargetCursor.transform.position = TargetTile.GetComponent<TileContainer>().entityOnTile.transform.position;
        }
    }
}
