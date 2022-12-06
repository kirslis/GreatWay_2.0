using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceContainerScript : MonoBehaviour
{
    [SerializeField] VariantMenuSkript _spellVariantPanel;
    [SerializeField] VariantMenuSkript _basicActionVariantPanel;
    [SerializeField] HealthBarScript _healthBar;

    public VariantMenuSkript spellVariantPanel { get { return _spellVariantPanel; } }
    public VariantMenuSkript basicAtionVariantPanel { get { return _basicActionVariantPanel; } }

    public int maxHp { set { _healthBar.maxHp = value; } }
    public int currentHp { set { _healthBar.currentHP = value; } }
    public int temporaryHP { set { _healthBar.temporaryHP = value; } }

    public void UpdateStatsView()
    {
        _healthBar.UpdateHP();
    }
}
