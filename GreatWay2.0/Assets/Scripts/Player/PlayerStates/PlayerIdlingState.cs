using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlingState : PlayerState
{
    public override IEnumerator Start()
    {
        return base.Start();
    }

    public override IEnumerator OnLeftMouseDown()
    {
        return base.OnLeftMouseDown();
    }

    public override IEnumerator OnSpaceDown()
    {
        return base.OnSpaceDown();
    }
}
