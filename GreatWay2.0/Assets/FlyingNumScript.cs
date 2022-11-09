using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyingNumScript : ProjectileFromTo
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
        {
            other.transform.GetComponent<ResultViuverScript>().AddValue(Value);
            Destroy(gameObject);
        }
    }
}
