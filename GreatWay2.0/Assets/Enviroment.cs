using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.XR.TrackedPoseDriver;

public class Enviroment : MonoBehaviour
{
    [SerializeField] protected bool _pathBlocking;
    [SerializeField] protected bool _visionBlocking;
    [SerializeField] protected bool _isAttackBlocking;
    [SerializeField] bool _isStatic;
    [SerializeField] bool _isInteracteble;

    private GridContainer Grid;
    protected delegate void Action();
    protected List<Action> Actions = new List<Action>();


    public bool isInteracteble { get { return _isInteracteble; } }
    public bool isStatic { get { return _isStatic; } }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Grid = FindObjectOfType<GridContainer>();
        UpdateTile();
    }

    protected void UpdateTile()
    {
        Grid.GetTile(transform.position).isPasseble = !_pathBlocking;
        Grid.GetTile(transform.position).isSeeThrought = !_visionBlocking;
        Grid.GetTile(transform.position).isAttackThrought = !_isAttackBlocking;
    }

    virtual public void OpenClose()
    {
        UpdateTile();
        FindObjectOfType<GlobalVisionController>().AllLookOut();
    }
}
