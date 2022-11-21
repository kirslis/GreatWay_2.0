using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : BasicMenu
{
    [SerializeField] BasicMenu MainMenu;
    [SerializeField] List<Button> Buttons;
    [SerializeField] GameObject _antitiesEnterfaceFolder;

    private bool IsActive = true;

    public bool isEnterfaceActive { set { SetEntarfaceActive(value); } }
    public GameObject antitiesEnterfaceFolder { get { return _antitiesEnterfaceFolder; } }

    private void SetEntarfaceActive(bool isActive)
    {
        foreach (Button button in Buttons)
            button.enabled = isActive;

        IsActive = isActive;
    }

    public override void OpenParentMenu()
    {
        if (IsActive)
            OpenNewMenu(MainMenu);
    }
}
