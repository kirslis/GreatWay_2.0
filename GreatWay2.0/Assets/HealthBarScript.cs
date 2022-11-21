using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Text _valueViewer;
    [SerializeField] Image _grayPart;

    private int MaxHp;
    private int CurrentHP;

    public int maxHp { set { MaxHp = value; } }
    public int currentHP { set { CurrentHP = value; } }

    public void UpdateHP()
    {
        _valueViewer.text = CurrentHP.ToString() + "\\" + MaxHp.ToString();
        _grayPart.fillAmount = 1 - (float)CurrentHP / MaxHp;
    }
}
