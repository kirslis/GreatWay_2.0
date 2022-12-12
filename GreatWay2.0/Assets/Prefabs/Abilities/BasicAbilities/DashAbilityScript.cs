using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dash Ability", menuName = "Dash Ability", order = 51)]
public class DashAbilityScript : BasicAbilityScript
{
    [SerializeField] DestroyTimer paticles;
    public override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        Caster.GetComponent<PlayerMove>().Dash();
        DestroyTimer Part =  Instantiate(paticles, Caster.transform.position, Quaternion.identity);
        Part.destroyTime = 1;

        base.Use();
    }
}
