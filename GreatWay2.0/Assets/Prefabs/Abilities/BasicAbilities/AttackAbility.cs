using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Attack Ability", menuName = "Attack Ability", order = 51)]
public class AttackAbility : BasicAbilityScript
{
    private BasicWeapon Weapon;

    public BasicWeapon weapon
    {
        set
        {
            Weapon = value;
            _range = Weapon.range;
            _abilityName = Weapon._name + " Attack";
            if (!Weapon.weaponMods.Contains(DataTypeHolderScript.WeaponMod.ranged))
                _areaTypeTag = "mili";
            else
                _areaTypeTag = "single";
        }
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        List<DataTypeHolderScript.TargetAntity> targets = Area.targets;
        Debug.Log("DAMAGE!");
        foreach (DataTypeHolderScript.TargetAntity target in targets)
        {
            Debug.Log(target);


            Vector3 StartPos = new Vector3(Caster.transform.position.x, Caster.transform.position.y + Caster.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2, Caster.transform.position.z);

            //Weapon.DealDamageFast(caster, )
        }
        base.Use();
    }

    public override AbilityButton AddNewCoreButton(AbilityController Player)
    {
        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        (NewButton as SimpleAttackButon).weapon = Weapon;
        CoreButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }

    public override AbilityButton AddNewSubButton(AbilityController Player)
    {
        if (SubButtons.Count > 0 && TryGetSubButton(Player, out ButtonPin button))
            DeleteSubButton(button);

        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        (NewButton as SimpleAttackButon).weapon = Weapon;
        SubButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }
}
