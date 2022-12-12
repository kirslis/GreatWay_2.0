using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsPanel : MonoBehaviour
{
    [SerializeField] GameObject _buttonContainer;
    [SerializeField] Button _basicButton;

    public delegate void Action();
    public List<Action> Actions = new List<Action>();

    public void OpenInteractMenu(List<Action> newActions)
    {

    }
}
