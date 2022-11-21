using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Damage Ability", menuName = "Basic Damage Ability", order = 51)]
public class BasicDamageAbilityScript : BasicAbilityScript
{
    [SerializeField] int _countOfDamageDices;
    [SerializeField] int _damageDiceType;
    [SerializeField] string _damageType;
    [SerializeField] string _abilityName;

    public override void Use()
    {
        List<Antity> targets = Area.targets;
        Debug.Log("DAMAGE!");
        foreach (Antity target in targets)
        {
            Debug.Log("POW!");
            int Damage = 0;
            for (int i = 0; i < _countOfDamageDices; i++)
                Damage = Random.Range(1, _damageDiceType + 1);

            target.GetComponent<CharacterStats>().DealDamage(Damage, _damageType);
        }
    }
}
