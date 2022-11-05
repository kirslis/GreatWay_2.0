using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MapRedactorMenu : BasicMenu
{
    [SerializeField] GlobalVisionController _globalVision;
    [SerializeField] Button _variantMenuButton;
    [SerializeField] Sprite _variantMenuButtonSpriteSwap;
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _buttonsContainer;
    [SerializeField] GameObject _standartOpenedVarianPanel;

    private GameObject OpenedVatiantPanel;
    private bool IsOpen = false;

    override protected void OnEnable()
    {
        base.OnEnable();
        _globalVision.EnterEditMode();
    }

    override protected void OnDisable()
    {
        base.OnDisable();
        _globalVision.ExitEditMode();
    }

    override protected void Awake()
    {
        base.Awake();
        OpenedVatiantPanel = _standartOpenedVarianPanel;
        OpenedVatiantPanel.SetActive(true);
    }

    public void VariantMenuButtonClick()
    {
        if (!IsOpen)
            StartCoroutine(ChangeSizeVarianMenuCoroutine(
                 (_buttonsContainer.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + _buttonsContainer.GetComponent<VerticalLayoutGroup>().spacing) * _buttonsContainer.transform.childCount + _buttonsContainer.GetComponent<VerticalLayoutGroup>().padding.top
                 ));
        else
            StartCoroutine(ChangeSizeVarianMenuCoroutine(0));
    }

    private void SwapButtonSprite()
    {
        Sprite t = _variantMenuButton.GetComponent<Image>().sprite;
        _variantMenuButton.GetComponent<Image>().sprite = _variantMenuButtonSpriteSwap;
        _variantMenuButtonSpriteSwap = t;
        _variantMenuButton.enabled = true;
    }

    IEnumerator ChangeSizeVarianMenuCoroutine(float targetHeight)
    {
        _variantMenuButton.enabled = false;
        float speed = 300f;
        RectTransform Rect = _panel.GetComponent<RectTransform>();

        while (Rect.sizeDelta.y != targetHeight)
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, new Vector2(Rect.sizeDelta.x, targetHeight), speed * Time.deltaTime);
            yield return null;
        }

        SwapButtonSprite();
        IsOpen = !IsOpen;
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
        error.StartFly(text, Mouse.current.position.ReadValue());
    }
}
