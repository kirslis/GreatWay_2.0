using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDescription : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] string _grayDescription;
    protected string BlackDescription;

    public string Name { get { return _name; } }
    public string grayDescription { get { return _grayDescription; } }
    virtual public string blackDescription{ get { return BlackDescription; } }

}
