using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMenu : MonoBehaviour
{
    [SerializeField] private GameObject _errorTextPlace;
    [SerializeField] protected ErrorText _errorText;

    private BasicMenu ParentMenu;
    private MenuActions Actions;
    protected Camera Cam;

    public BasicMenu parentMenu { set { ParentMenu = value;  } }

    protected virtual void Awake()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        Actions = new MenuActions();
        Actions.MenuesAction.OpenParent.performed += context => OpenParentMenu();
        Actions.Enable();
    }

    virtual protected void OnEnable()
    {
        Actions.Enable();
    }

    virtual protected void OnDisable()
    {
        Actions.Disable();
    }

    virtual public void OpenParentMenu()
    {
        ParentMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    protected void OpenNewMenu(BasicMenu newMenu)
    {
        newMenu.gameObject.SetActive(true);
        newMenu.parentMenu = this;
        gameObject.SetActive(false);
    }

    public virtual void ThrowErrorText(string text)
    {
        ErrorText error = Instantiate(_errorText, transform);
        error.StartFly(text, _errorTextPlace.transform.position);
    }
}
