using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface : BasicMenu
{
    [SerializeField] BasicMenu MainMenu;

    public override void OpenParentMenu()
    {
        OpenNewMenu(MainMenu);
    }
}
