using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceContainerScript : MonoBehaviour
{
    [SerializeField] VariantMenuSkript _basicActionsVariantPanel;
    [SerializeField] HealthBarScript _healthBar;

    public VariantMenuSkript basicActionsVariantPanel { get { return _basicActionsVariantPanel; } }

    public int maxHp { set { _healthBar.maxHp = value; } }
    public int currentHp { set { _healthBar.currentHP = value; } }

    public void UpdateStatsView()
    {
        _healthBar.UpdateHP();
    }
}
