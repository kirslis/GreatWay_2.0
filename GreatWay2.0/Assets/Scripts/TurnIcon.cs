using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIcon : MonoBehaviour
{
    bool IsVisible = false;
    private Antity Parrent;

    public bool isVisible { get { return IsVisible; } set { IsVisible = value; } }
    public Antity parrent { get { return Parrent; } set { Parrent = value; GetComponent<Image>().sprite = Parrent.GetComponent<CharacterStats>().image; } }

}
