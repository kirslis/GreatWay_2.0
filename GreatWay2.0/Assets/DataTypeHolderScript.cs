using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Type Holder", menuName = "Data Type Holder", order = 51)]
public class DataTypeHolderScript : ScriptableObject
{
    public enum DamageType
    {
        fire,
        cold
    }

    public enum AttackType
    {
        magic,
        range,
        mili
    }

    public enum DiceType
    {
        d4,
        d6,
        d8,
        d10,
        d12,
        d20
    }

    public enum WeaponType
    {
        military,
        simple
    }

    public enum WeaponMod
    {
        ranged,
        reaching,
        fencing
    }

    public enum AbiltyType
    {
        basicAbility,
        classAbility,
        spell
    }

    public enum ActiveType
    {
        mainActive,
        subActive
    }

    public struct TargetAntity
    {
        public Antity Target;
        public int TypeOfTarget;

        public TargetAntity(Antity target, int typeOfTile)
        {
            Target = target;
            TypeOfTarget = typeOfTile;
        }
    }
}
