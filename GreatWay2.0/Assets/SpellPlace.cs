using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPlace : MonoBehaviour
{
    private AbilityButton CurrentButton = null;
    private AbilityManager AbilityManager;

    void Start()
    {
        AbilityManager = FindObjectOfType<AbilityManager>();
        //if (CurrentButton != null)
        //    GetComponent<Image>().sprite = CurrentButton.GetComponent<Image>().sprite;
    }

    public void ChangeSpell(AbilityButton Spell, AbilityController Player)
    {
        if (CurrentButton != null)
        {
            AbilityManager.DeleteSubButton(CurrentButton);
        }

        if (Spell.ability is AttackAbility)
            CurrentButton = AbilityManager.CreateNewSubAttackButton(Spell.ability as AttackAbility, Player);
        else 
            CurrentButton = AbilityManager.CreateNewSubButton(Spell.ability, Player);

        Debug.Log(CurrentButton);
        CurrentButton.transform.SetParent(transform, false);
        CurrentButton.transform.position = transform.position;


        //if (CurrentButton != null)
        //    CurrentButton.DeleteButton();

        //CurrentButton = Instantiate(Spell, transform);
        //CurrentButton.ability = Spell.ability;
        //CurrentButton.ability.AddNewSubButton(CurrentButton);
    }

    private void UpdateVision()
    {

    }
}
