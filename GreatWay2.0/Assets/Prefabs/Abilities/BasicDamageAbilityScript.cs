using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Damage Ability", menuName = "Basic Damage Ability", order = 51)]
public class BasicDamageAbilityScript : BasicAbilityScript
{
    [SerializeField] int _countOfDamageDices;
    [SerializeField] int _damageDiceType;
    [SerializeField] DataTypeHolderScript.DamageType _damageType;
    [SerializeField] string _abilityName;
    [SerializeField] bool _isGaranteedHit;
    [SerializeField] BasicShotenParticleScript _shotenParticle;
    [SerializeField] DataTypeHolderScript.AttackType _attackType = DataTypeHolderScript.AttackType.magic;

    public override void Use()
    {
        List<Antity> targets = Area.targets;
        Debug.Log("DAMAGE!");
        foreach (Antity target in targets)
        {
            Debug.Log(target);
            

            Vector3 StartPos = new Vector3(Caster.transform.position.x, Caster.transform.position.y + Caster.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2, Caster.transform.position.z);

            BasicShotenParticleScript particle = Instantiate(_shotenParticle, StartPos, Quaternion.identity);
            particle.target = target;
            particle.ability = this;
        }
        Abort();
    }

    public virtual void DealDamage(Antity target)
    {
        int Damage = 0;
        for (int i = 0; i < _countOfDamageDices; i++)
            Damage = Random.Range(1, _damageDiceType + 1);

        target.GetComponent<CharacterStats>().DealDamage(Damage, _damageType, _attackType, Caster.GetComponent<CharacterStats>());

    }
}
