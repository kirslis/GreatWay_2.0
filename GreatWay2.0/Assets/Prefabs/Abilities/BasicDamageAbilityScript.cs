using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Damage Ability", menuName = "Basic Damage Ability", order = 51)]
public class BasicDamageAbilityScript : BasicAbilityScript
{
    [SerializeField] int _countOfDamageDices;
    [SerializeField] int _damageDiceType;
    [SerializeField] DataTypeHolderScript.DamageType _damageType;
    [SerializeField] bool _isGaranteedHit;
    [SerializeField] BasicShotenParticleScript _shotenParticle;
    [SerializeField] DataTypeHolderScript.AttackType _attackType = DataTypeHolderScript.AttackType.magic;

    public override IEnumerator TryToUse(AbilityButton button)
    {
        if (ActivateCheck())
        {
            yield return AbilityUse();
            yield return base.TryToUse(button);
        }
    }

    protected override IEnumerator AbilityUse()
    {
        List<DataTypeHolderScript.TargetAntity> targets = Area.targets;
        Debug.Log("DAMAGE!");
        foreach (DataTypeHolderScript.TargetAntity target in targets)
        {
            int attackRoll;
            bool isHitting;

            if (!_isGaranteedHit)
            {
                if (target.TypeOfTarget == 1)
                {
                    int firstRoll = Random.Range(1, 21);
                    int secondRoll = Random.Range(1, 21);
                    attackRoll = Mathf.Min(firstRoll, secondRoll);
                }
                else
                    attackRoll = Random.Range(1, 21);

                isHitting = attackRoll >= target.Target.GetComponent<CharacterStats>().ac;
                AbilityManager.RollMassage(Caster.transform.position, "attack roll", attackRoll, Color.red);
            }
            else
                isHitting = true;


            Vector3 StartPos = new Vector3(Caster.transform.position.x, Caster.transform.position.y + Caster.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2, Caster.transform.position.z);

            BasicShotenParticleScript particle = Instantiate(_shotenParticle, StartPos, Quaternion.identity);

            particle.target = target.Target;

            particle.ability = this;
            particle.isHitting = isHitting;

            yield return particle.Play();
        }

        yield return base.AbilityUse();
    }

    public virtual void DealDamage(Entity target)
    {
        int Damage = 0;
        for (int i = 0; i < _countOfDamageDices; i++)
            Damage = Random.Range(1, _damageDiceType + 1);

        target.GetComponent<CharacterStats>().DealDamage(Damage, _damageType, _attackType, Caster.GetComponent<CharacterStats>());

    }
}
