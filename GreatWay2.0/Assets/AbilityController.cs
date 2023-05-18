using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] protected List<BasicAbilityScript> _spells;
    [SerializeField] protected List<BasicAbilityScript> _actions;

    protected AbilityManager AbilityFolder;
    //private 

    protected virtual void Awake()
    {
        AbilityFolder = FindObjectOfType<AbilityManager>();
    }

    public virtual void AddAction(BasicAbilityScript action)
    {
    }

    public virtual void AddAttack(BasicWeapon weapon)
    {
    }

}
