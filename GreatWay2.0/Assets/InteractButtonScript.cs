using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractButtonScript : MonoBehaviour
{
    private GameObject Target = null;
    private InteractListinger.InteractOption action;

    public void SetTarget(GameObject target)
    {
        Target = target;
        GetComponentInChildren<Text>().text = target.GetComponent<BasicDescription>().Name;
    }

    public void SetAction(InteractListinger.InteractOption a, string Name)
    {
        action = a;
        GetComponentInChildren<Text>().text = Name;
    }

    public void OnClick()
    {
        if (Target != null)
            GetComponentInParent<InteractMenuScript>().ShowInteract(Target);
        else
        {
            GetComponentInParent<InteractMenuScript>().Close();
            action();
        }
    }
}
