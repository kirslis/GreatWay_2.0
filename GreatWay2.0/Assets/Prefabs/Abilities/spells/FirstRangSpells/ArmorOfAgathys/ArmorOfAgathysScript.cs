using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Of Agathys", menuName = "Armor Of Agathys", order = 51)]
public class ArmorOfAgathysScript : DamageIncomingBuff
{
    [SerializeField] DestroyTimer DamageParticles;
    private float DamageCourutineTime = 0.3f;

    public override void Use()
    {
        List<Antity> targets = Area.targets;
        foreach (Antity target in targets)
        {
            target.GetComponent<CharacterStats>().temporaryHP = 5;
        }

        base.Use();
    }

    protected override void DamageIncoming(ref int Damage, DataTypeHolderScript.DamageType DamageType, DataTypeHolderScript.AttackType AttackType, CharacterStats Person, CharacterStats DamageDealer)
    {
        if (DamageDealer != null && AttackType == DataTypeHolderScript.AttackType.mili)
        {
            Instantiate(DamageParticles, Person.transform);
            DamageParticles.destroyTime = DamageCourutineTime;
            DamageDealer.DealDamage(5, DataTypeHolderScript.DamageType.cold, AttackType, null);
        }
    }

    override protected void HurtCheck(CharacterStats person)
    {
        if (person.temporaryHP == 0)
            base.HurtCheck(person);
    }
}
