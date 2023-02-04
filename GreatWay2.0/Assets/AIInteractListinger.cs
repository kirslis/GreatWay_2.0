using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteractListinger : InteractListinger
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Options.Add(new KeyValuePair<string, InteractOption>("redact patrole trail", RedactPatroleTrail));
    }

    public void RedactPatroleTrail()
    {
        Debug.Log("Trailing");
        GetComponent<PatroleTrailRedactor>().CreatePatrouleTrail();
    }
}
