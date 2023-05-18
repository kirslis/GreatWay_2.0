using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : CharacterStats
{
    public override void DealDamage(int damage, DataTypeHolderScript.DamageType DamageType, DataTypeHolderScript.AttackType attackType, CharacterStats DamageDealer)
    {
        Debug.Log("Deal damage AI");
        GetComponent<AIStateMachine>().AddEnemyToList(DamageDealer.GetComponent<Entity>(), damage, 0);
        base.DealDamage(damage, DamageType, attackType, DamageDealer);
    }
}
