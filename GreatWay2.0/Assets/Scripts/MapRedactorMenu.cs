using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MapRedactorMenu : BasicMenu
{
    [SerializeField] GlobalVisionController _globalVision;
    [SerializeField] GameObject _standartOpenedVarianPanel;
    [SerializeField] DescriptionViewer _descriptionViewer;

    private GameObject OpenedVatiantPanel;

    override protected void OnEnable()
    {
        base.OnEnable();
        _globalVision.EnterEditMode();
    }

    override protected void OnDisable()
    {
        base.OnDisable();
        _globalVision.ExitEditMode();
        _descriptionViewer.gameObject.SetActive(false);
    }

    override protected void Awake()
    {
        base.Awake();
        OpenedVatiantPanel = _standartOpenedVarianPanel;
        OpenedVatiantPanel.SetActive(true);
    }



    public void ChangeVariantTipe(GameObject VariantPanel)
    {
        if (OpenedVatiantPanel != null)
            OpenedVatiantPanel.SetActive(false);
        OpenedVatiantPanel = VariantPanel;
        OpenedVatiantPanel.SetActive(true);
    }

    public override void ThrowErrorText(string text)
    {
        ErrorText error = Instantiate(_errorText, transform);
        error.transform.position = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        error.transform.position = new Vector3(error.transform.position.x, error.transform.position.y, 0);
        error.StartFly(text, Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }
}
