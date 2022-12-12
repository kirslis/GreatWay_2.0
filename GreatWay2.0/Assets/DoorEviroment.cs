using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEviroment : WindowEnviroment
{
    public override void OpenClose()
    {
        _pathBlocking = !_pathBlocking;
        base.OpenClose();
    }
}
