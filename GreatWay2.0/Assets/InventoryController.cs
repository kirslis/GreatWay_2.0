using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] BasicWeapon _mainWeapon;
    [SerializeField] BasicWeapon _subWeapon;

    private ItemState MainWeapon;
    private ItemState SubWeapon;

    private struct ItemState
    {
        public BasicWeapon weapon;
    }

    private List<ItemState> _items;

    private void Awake()
    {
        MainWeapon.weapon =  _mainWeapon;
        GetComponent<AbilityController>().AddAttack(MainWeapon.weapon);
        SubWeapon.weapon = _subWeapon;
        GetComponent<AbilityController>().AddAttack(SubWeapon.weapon);
        //Instantiate(_subWeapon, transform);
    }
}
