using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDescription : BasicDescription
{
    public override string blackDescription
    {
        get
        {
            string Desc = gameObject.name + "\n" +
                "path cost = " + GetComponent<BasicTile>().basePathCost;
            return Desc;
        }
    }
}
