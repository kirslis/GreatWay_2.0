using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] List<BasicAbilityScript> _abilities;
    [SerializeField] UiController _playerUIController;

    private AbilityManager AbilityFolder;

    private void Awake()
    {
        AbilityFolder = FindObjectOfType<AbilityManager>();

        foreach (BasicAbilityScript ability in _abilities)
        {
            AbilityButton NewButton = AbilityFolder.CreateNewCoreAbilityButton(ability, this);
            NewButton.transform.parent = _playerUIController.playerInteface.basicActionsVariantPanel.buttonContainer.transform;
        }
    }

}
