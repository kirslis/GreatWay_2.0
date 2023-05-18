using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : AbilityController
{
    [SerializeField] protected UiController _playerUIController;
    override protected void Awake()
    {
        base.Awake();

        foreach (BasicAbilityScript ability in _spells)
        {
            AbilityButton NewButton = AbilityFolder.CreateNewCoreSpellButton(ability, this);
            NewButton.transform.parent = _playerUIController.playerInteface.spellVariantPanel.buttonContainer.transform;
        }

        foreach (BasicAbilityScript ability in _actions)
        {
            AbilityButton NewButton = AbilityFolder.CreateNewCoreAbilityButton(ability, this);
            NewButton.transform.parent = _playerUIController.playerInteface.basicAtionVariantPanel.buttonContainer.transform;
        }
    }

    public override void AddAction(BasicAbilityScript action)
    {
    }

    public override void AddAttack(BasicWeapon weapon)
    {
        AbilityFolder.AddAttackAbility(weapon, this).transform.parent = _playerUIController.playerInteface.basicAtionVariantPanel.buttonContainer.transform;
    }
}
