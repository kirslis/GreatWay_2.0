using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] List<BasicAbilityScript> _spells;
    [SerializeField] List<BasicAbilityScript> _actions;
    [SerializeField] UiController _playerUIController;

    private AbilityManager AbilityFolder;
    //private 

    private void Awake()
    {
        AbilityFolder = FindObjectOfType<AbilityManager>();

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

    public void AddAction(BasicAbilityScript action)
    {
    }

    public void AddAttack(BasicWeapon weapon)
    {
        AbilityFolder.AddAttackAbility(weapon, this).transform.parent = _playerUIController.playerInteface.basicAtionVariantPanel.buttonContainer.transform;
    }

}
