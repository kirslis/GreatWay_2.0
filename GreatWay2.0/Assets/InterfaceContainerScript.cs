using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceContainerScript : MonoBehaviour
{
    [SerializeField] VariantMenuSkript _spellVariantPanel;
    [SerializeField] VariantMenuSkript _basicActionVariantPanel;
    [SerializeField] HealthBarScript _healthBar;
    [SerializeField] Text _aCViwer;
    [SerializeField] Image _mainActionPointViwer;
    [SerializeField] Image _subActionPointViwer;

    public VariantMenuSkript spellVariantPanel { get { return _spellVariantPanel; } }
    public VariantMenuSkript basicAtionVariantPanel { get { return _basicActionVariantPanel; } }

    public int maxHp { set { _healthBar.maxHp = value; } }
    public int currentHp { set { _healthBar.currentHP = value; } }
    public int temporaryHP { set { _healthBar.temporaryHP = value; } }
    public int aC { set { _aCViwer.text = value.ToString(); } }
    public bool mainActionPoint { set { if (value) _mainActionPointViwer.color = Color.green; else _mainActionPointViwer.color = Color.white; } }
    public bool subActionPoint { set { if (value) _subActionPointViwer.color = Color.blue; else _subActionPointViwer.color = Color.white; } }

    public void UpdateStatsView()
    {
        _healthBar.UpdateHP();
    }
}
