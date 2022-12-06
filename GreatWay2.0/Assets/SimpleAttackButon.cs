using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAttackButon : AbilityButton
{
    private BasicWeapon Weapon;

    public BasicWeapon weapon { set { name = value.name; Weapon = value; GetComponent<Image>().sprite = value._buttonImage;  SmallButton.sprite = value._buttonImage; } }


}
