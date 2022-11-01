using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapEditorMune : BasicMenu
{
    [SerializeField] BasicMenu _mapCreatorMenu;
    [SerializeField] BasicMenu _mapRedactorMenu;
    [SerializeField] MapController _map;

    public void OpenMapCreatorMenu()
    {
        OpenNewMenu(_mapCreatorMenu);
    }

    public void OpenMapRedactorMenu()
    {
        if (_map.isGenerated)
            OpenNewMenu(_mapRedactorMenu);
        else
            ThrowErrorText("First create the map :\\");    
        }
}
