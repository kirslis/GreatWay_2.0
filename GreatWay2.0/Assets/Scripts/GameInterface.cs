using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : BasicMenu
{
    [SerializeField] BasicMenu MainMenu;
    [SerializeField] List<Button> Buttons;
    [SerializeField] GameObject _entitiesEnterfaceFolder;
    [SerializeField] EntityContainer _entityContainer;

    private bool IsActive = true;

    public bool isEnterfaceActive { set { SetEntarfaceActive(value); } }
    public GameObject entitiesEnterfaceFolder { get { return _entitiesEnterfaceFolder; } }

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

    protected override void OnEnable()
    {
        if (_entityContainer.antityes.Count > 0)
            _entityContainer.currentPlayer.GetComponent<PlayerController>().InputMode(true);
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        if (_entityContainer.antityes.Count > 0)
            _entityContainer.currentPlayer.GetComponent<PlayerController>().InputMode(false);
        base.OnDisable();
    }
}
