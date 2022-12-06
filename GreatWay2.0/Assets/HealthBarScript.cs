using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Text _HPViewer;
    [SerializeField] Text _temporaryHPViewer;
    [SerializeField] Image _grayPart;
    [SerializeField] Image _TemporaryHPImage;

    private int MaxHp;
    private int CurrentHP;
    private int TemporaryHP = 0; 

        public int maxHp { set { MaxHp = value; } }
    public int currentHP { set { CurrentHP = value; } }
    public int temporaryHP { set { TemporaryHP = value; } } 

    public void UpdateHP()
    {
        _HPViewer.text = CurrentHP.ToString() + "\\" + MaxHp.ToString();
        _grayPart.fillAmount = 1 - (float)CurrentHP / MaxHp;
        if (TemporaryHP == 0)
        {
            _TemporaryHPImage.gameObject.SetActive(false);
            _temporaryHPViewer.gameObject.SetActive(false);
        }
        else
        {
            _TemporaryHPImage.gameObject.SetActive(true);
            _temporaryHPViewer.gameObject.SetActive(true);
        }

        _temporaryHPViewer.text = TemporaryHP.ToString();

    }
}
