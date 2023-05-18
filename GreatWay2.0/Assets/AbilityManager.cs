using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] List<BasicUsibleTargetAreaViewer> _areaViewers = new List<BasicUsibleTargetAreaViewer>();
    [SerializeField] List<BasicAbilityScript> _spells;
    [SerializeField] SimpleAttackButon _simpleAttackButton;
    [SerializeField] List<BasicAbilityScript> _actions;
    [SerializeField] ErrorText _errorText;

    private List<AttackAbility> Attacks = new List<AttackAbility>();

    private BasicAbilityScript CurentAbility = null;
    public BasicAbilityScript curentAbility { set { if (CurentAbility != null && value != null) CurentAbility.Abort(); CurentAbility = value; } }

    private void Awake()
    {
        foreach (BasicUsibleTargetAreaViewer area in _areaViewers)
            area.Awake();

        foreach (BasicAbilityScript ability in _spells)
        {
            int i = 0;
            while (i < _areaViewers.Count && _areaViewers[i]._typeTag != ability._areaTypeTag)
                i++;

            ability.abilityManager = this;
            ability.area = _areaViewers[i];
            ability.Awake();
        }

        foreach (BasicAbilityScript ability in _actions)
        {
            int i = 0;
            while (i < _areaViewers.Count && _areaViewers[i]._typeTag != ability._areaTypeTag)
                i++;

            ability.abilityManager = this;
            ability.area = _areaViewers[i];
            ability.Awake();
        }
    }

    public BasicAbilityScript GetAbilityLink(BasicAbilityScript Ability)
    {
        Debug.Log("Create AI spell");
        if (Ability.type == DataTypeHolderScript.AbiltyType.basicAbility)
        {
            foreach (BasicAbilityScript script in _actions)
                if (Ability.Equals(script))
                    return script;
        }

        else if (Ability.type == DataTypeHolderScript.AbiltyType.spell)
        {
            foreach (BasicAbilityScript script in _spells)
                if (Ability.Equals(script))
                    return script;
        }

        Debug.Log("AI spell not found");
        return null;
    }

    public AbilityButton CreateNewCoreSpellButton(BasicAbilityScript Ability, AbilityController Player)
    {
        Debug.Log("Create Spell");
        foreach (BasicAbilityScript script in _spells)
            if (Ability.Equals(script))
                return script.AddNewCoreButton(Player);

        Debug.Log("Spell Not Found");
        return null;
    }

    public AbilityButton CreateNewCoreAbilityButton(BasicAbilityScript Ability, AbilityController Player)
    {
        foreach (BasicAbilityScript script in _actions)
            if (Ability.Equals(script))
                return script.AddNewCoreButton(Player);

        Debug.Log("Spell Not Found");
        return null;
    }

    public AbilityButton CreateNewSubButton(BasicAbilityScript Ability, AbilityController Player)
    {
        if (Ability.type == DataTypeHolderScript.AbiltyType.basicAbility)
        {
            foreach (BasicAbilityScript script in _actions)
                if (Ability.Equals(script))


                    return script.AddNewSubButton(Player);
        }

        else if (Ability.type == DataTypeHolderScript.AbiltyType.spell)
        {
            foreach (BasicAbilityScript script in _spells)
                if (Ability.Equals(script))
                {
                    return script.AddNewSubButton(Player);
                }
        }


        Debug.Log("Not Ok");
        return null;
    }

    public void DeleteSubButton(AbilityButton Button)
    {
        foreach (BasicAbilityScript script in _spells)
            if (script.Equals(Button.ability))
                script.DeleteButton(Button);
    }

    public AbilityButton AddAttackAbility(BasicWeapon Weapon, AbilityController Player)
    {
        AttackAbility newAttack = ScriptableObject.CreateInstance<AttackAbility>();
        newAttack.weapon = Weapon;
        newAttack._baseAbilityButton = _simpleAttackButton;
        Debug.Log("================" + newAttack._baseAbilityButton);

        int i = 0;
        while (i < _areaViewers.Count && _areaViewers[i]._typeTag != newAttack._areaTypeTag)
            i++;

        newAttack.abilityManager = this;
        newAttack.area = _areaViewers[i];
        newAttack.Awake();

        Attacks.Add(newAttack);

        return newAttack.AddNewCoreButton(Player);
    }

    public AbilityButton CreateNewSubAttackButton(AttackAbility Ability, AbilityController Player)
    {
        foreach (AttackAbility attack in Attacks)
            if (Ability.Equals(attack))
            {
                return attack.AddNewSubButton(Player);
            }

        Debug.Log("Not Ok");
        return null;
    }

    public void RollMassage(Vector3 Pos, string Massage, int value, Color color)
    {
        ErrorText text = Instantiate(_errorText);
        text.GetComponent<TextMeshPro>().color = color;
        text.StartFly(Massage + " " + value.ToString(), new Vector3(Pos.x, Pos.y + 0.5f, -3));
    }
}
