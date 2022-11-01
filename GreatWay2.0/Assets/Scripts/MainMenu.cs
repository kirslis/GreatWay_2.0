using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BasicMenu
{
    [SerializeField] BasicMenu _editMapMenu;

    public void OpenCreateMenu()
    {
        OpenNewMenu(_editMapMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
