using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVisionConroller : VisionController
{
    public List<BasicTile> visibleTiles { get { return VisibleTiles; } }

    public override void LookOut()
    {
        base.LookOut();
        GetComponent<AIStateMachine>().LookOut();
    }
}
