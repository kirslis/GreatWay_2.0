using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] List<BasicUsibleTargetAreaViewer> _areaViewers = new List<BasicUsibleTargetAreaViewer>();
    [SerializeField] List<BasicAbilityScript> _abilities;

    private void Awake()
    {
        foreach(BasicUsibleTargetAreaViewer area in _areaViewers)
            area.Awake();

        foreach (BasicAbilityScript ability in _abilities)
        {
            int i = 0;
            while (i < _areaViewers.Count && _areaViewers[i]._typeTag != ability._areaTypeTag)
                i++;

            ability.area = _areaViewers[i];
            ability.Awake();
        }
    }

    public AbilityButton CreateNewCoreAbilityButton(BasicAbilityScript Ability, AbilityController Player)
    {
        foreach (BasicAbilityScript script in _abilities)
            if (Ability.Equals(script))
            {
                Debug.Log("OK");
                return script.AddNewCoreButton(Player);
            }

        Debug.Log("Not Ok");
        return null;
    }

    public AbilityButton CreateNewSubAbilityButton(BasicAbilityScript Ability, AbilityController Player)
    {
        foreach (BasicAbilityScript script in _abilities)
            if (Ability.Equals(script))
            {
                Debug.Log("OK");
                return script.AddNewSubButton(Player);
            }

        Debug.Log("Not Ok");
        return null;
    }

    public void DeleteSubButton(AbilityButton Button)
    {
        foreach(BasicAbilityScript script in _abilities)
            if(script.Equals(Button.ability))
                script.DeleteButton(Button);
    }
}
