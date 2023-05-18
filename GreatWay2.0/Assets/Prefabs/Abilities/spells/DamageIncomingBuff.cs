using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Incoming Buff", menuName = "Damage Incming Buff", order = 51)]

public class DamageIncomingBuff : BasicAbilityScript
{
    [SerializeField] BasicBuffParticle _particles;

    public override IEnumerator TryToUse(AbilityButton button)
    {
        if (ActivateCheck())
        {
            List<DataTypeHolderScript.TargetAntity> targets = Area.targets;
            foreach (DataTypeHolderScript.TargetAntity target in targets)
            {
                target.Target.GetComponent<CharacterStats>().AddDamageIncomingModificator(DamageIncoming);
                target.Target.GetComponent<CharacterStats>().AddHurtListener(HurtCheck);
                BasicBuffParticle particles = Instantiate(_particles, target.Target.transform);
                yield return particles.Play();
            }
            yield return base.TryToUse(button);
        }
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
