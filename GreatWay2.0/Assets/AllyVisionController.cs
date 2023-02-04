using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyVisionController : VisionController
{
    public override void LookOut()
    {
        base.LookOut();
        GlobalVision.AddVisibleTiles(VisibleTiles);
    }
}
