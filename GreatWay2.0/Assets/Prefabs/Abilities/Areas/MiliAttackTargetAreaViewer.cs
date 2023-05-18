using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mili Attack Target Viewer", menuName = "Mili Attack Target Viewer", order = 51)]
public class MiliAttackTargetAreaViewer : SingleTargetAreaViewerScript
{
    // Start is called before the first frame update
    override public void LightUpTargetArea(Vector3 StartPos, int Range, string tag, bool needToLightUp)
    {
        NeedToLightUp = needToLightUp;
        ClearData();

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
                CheckTile(StartPos, Grid.GetTile(new Vector2(j, i)), tag);
    }
}
