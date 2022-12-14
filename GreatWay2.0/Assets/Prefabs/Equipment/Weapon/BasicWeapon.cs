using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 51)]
public class BasicWeapon : BasicEquip
{
    [SerializeField] public SimpleAttackButon _simpleAttackButon;
    [SerializeField] public Sprite _buttonImage;
    [SerializeField] public string _name;

    [SerializeField] DataTypeHolderScript.WeaponType _weaponType;
    [SerializeField] List<DataTypeHolderScript.DiceType> _damageDices;
    [SerializeField] List<DataTypeHolderScript.WeaponMod> _weaponMods;
    [SerializeField] DataTypeHolderScript.DamageType _damageType;
    [SerializeField] int _range = 1;
    [SerializeField] AttackAbility _attackAbility;

    private int AttackMod = 0;
    private int DamageMod = 0;

    public int range { get { return _range; } }
    public List<DataTypeHolderScript.WeaponMod> weaponMods { get { return _weaponMods; } }

    public void Equip(CharacterStats Person)
    {
        //Person.GetComponent<AbilityController>().
    }

    public void DealDamageFast(CharacterStats AttackPerson, DataTypeHolderScript.AttackType attackType, CharacterStats Target)
    {
        int SumDamage = 0;
        foreach (DataTypeHolderScript.DiceType damageType in _damageDices)
        {
            SumDamage += Random.Range(1, 9);
        }

        Target.DealDamage(SumDamage, _damageType, attackType, AttackPerson);
    }
}
