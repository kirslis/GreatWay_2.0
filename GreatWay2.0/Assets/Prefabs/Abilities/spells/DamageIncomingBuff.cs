using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Incoming Buff", menuName = "Damage Incming Buff", order = 51)]

public class DamageIncomingBuff : BasicAbilityScript
{
    [SerializeField] BasicBuffParticle _particles;

    public override void Use()
    {
        List<Antity> targets = Area.targets;
        foreach (Antity target in targets)
        {
            target.GetComponent<CharacterStats>().AddDamageIncomingModificator(DamageIncoming);
            target.GetComponent<CharacterStats>().AddHurtListener(HurtCheck);
            Instantiate(_particles, target.transform);
        }
        base.Use();
    }

    virtual protected void DamageIncoming(ref int Damage, DataTypeHolderScript.DamageType DamageType, DataTypeHolderScript.AttackType _attackType, CharacterStats Person, CharacterStats DamageDealer)
    {
    }

    virtual protected void HurtCheck(CharacterStats person)
    {
        person.DeleteDamageIncomingModificator(DamageIncoming);
        person.DeleteHurtListener(HurtCheck);
        Destroy(person.GetComponentInChildren<BasicBuffParticle>().gameObject);
    }
}
