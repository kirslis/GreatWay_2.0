using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAbilityController : AbilityController
{
    private List<BasicAbilityScript> actionLinks = new List<BasicAbilityScript>(); 

    protected override void Awake()
    {
        base.Awake();

        foreach (BasicAbilityScript script in _actions)
            actionLinks.Add(AbilityFolder.GetAbilityLink(script));

        foreach (BasicAbilityScript script in _spells)
            actionLinks.Add(AbilityFolder.GetAbilityLink(script));
    }

    public IEnumerator UseAction(string actionName, BasicTile targetTile)
    {
        int i = 0;
        Debug.Log("Count of actions - " + actionLinks.Count);

        while (i < actionLinks.Count && actionLinks[i].name != actionName)
            i++;

        if (i < actionLinks.Count)
            yield return actionLinks[i].UseOnTarget(targetTile, this);
        else
            Debug.Log("No action");
    }
}
