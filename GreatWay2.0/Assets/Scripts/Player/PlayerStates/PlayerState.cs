using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator OnLeftMouseDown()
    {
        yield break;
    }

    public virtual IEnumerator OnRightMouseDown()
    {
        yield break;
    }

    public virtual IEnumerator OnSpaceDown()
    {
        yield break;
    }

    public virtual IEnumerator End()
    {
        yield break;
    }
}
