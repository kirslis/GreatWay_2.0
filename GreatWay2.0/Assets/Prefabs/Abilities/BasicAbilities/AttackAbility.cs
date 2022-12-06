using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Attack Ability", menuName = "Attack Ability", order = 51)]
public class AttackAbility : BasicAbilityScript
{
    private BasicWeapon Weapon;

    public BasicWeapon weapon { set { Weapon = value; _range = Weapon.range; } }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
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
