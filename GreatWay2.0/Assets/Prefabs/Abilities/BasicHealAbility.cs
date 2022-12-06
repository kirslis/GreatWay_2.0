using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Heal Ability", menuName = "Basic Heal Ability", order = 51)]
public class BasicHealAbility : BasicAbilityScript
{
    [SerializeField] HealingParticlesScript _healingParticle;
    [SerializeField] int _countOfHealDices;
    [SerializeField] int _healDiceType;
    [SerializeField] string _abilityName;

    public override void Use()
    {
        List<Antity> targets = Area.targets;
        Debug.Log("Heal!");
        foreach (Antity target in targets)
        {
            Debug.Log(target);
            HealingParticlesScript healing = Instantiate(_healingParticle, target.transform.position, Quaternion.identity);
            healing.target = target;
            healing.ability = this;
        }
        Abort();
    }

    public void Heal(Antity target)
    {
        int Heal = 0;
        for (int i = 0; i < _countOfHealDices; i++)
            Heal += Random.Range(1, _healDiceType + 1);

        Debug.Log(Heal);
        target.GetComponent<CharacterStats>().Heal(Heal);
    }
}
