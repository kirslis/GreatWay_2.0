using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VariantMenuSkript : MonoBehaviour
{
    [SerializeField] Button _variantMenuButton;
    [SerializeField] Sprite _variantMenuButtonSpriteSwap;
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _buttonsContainer;
    [SerializeField] int _countOfButtonsInRow = 1;

    private VariiantPanelActions Action;
    private Camera Cam;
    private bool IsOpen = false;
    private BoxCollider collider;

    public GameObject buttonContainer { get { return _buttonsContainer; } }

    private void Awake()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        collider = GetComponent<BoxCollider>();

        Action = new VariiantPanelActions();
        Action.Opened.LMouse.performed += context =>
        {
            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);
            if (hit.collider == null || hit.collider.gameObject != gameObject)
                Close();
        };
    }

    public void VariantMenuButtonClick()
    {
        Debug.Log("CLICK");
        if (!IsOpen)
        {
            StartCoroutine(ChangeSizeVarianMenuCoroutine(
                 (_buttonsContainer.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + _buttonsContainer.GetComponent<VerticalLayoutGroup>().spacing) * _buttonsContainer.transform.childCount / _countOfButtonsInRow + (_buttonsContainer.transform.childCount % _countOfButtonsInRow != 0 ? 1 : 0) + _buttonsContainer.GetComponent<VerticalLayoutGroup>().padding.top
                 ));
            Action.Enable();
        }
        else
        {
            StartCoroutine(ChangeSizeVarianMenuCoroutine(0));
            Action.Disable();
        }
    }

    public void Close()
    {
        StartCoroutine(ChangeSizeVarianMenuCoroutine(0));
        Action.Disable();
    }

    IEnumerator ChangeSizeVarianMenuCoroutine(float targetHeight)
    {
        _variantMenuButton.enabled = false;
        float speed = 300f;
        RectTransform Rect = _panel.GetComponent<RectTransform>();

        while (Rect.sizeDelta.y != targetHeight)
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, new Vector2(Rect.sizeDelta.x, targetHeight), speed * Time.deltaTime);
            collider.size = Rect.sizeDelta;
            collider.center = new Vector2(0, collider.size.y / 2);
            yield return null;
        }

        SwapButtonSprite();
        IsOpen = !IsOpen;
    }

    private void SwapButtonSprite()
    {
        Sprite t = _variantMenuButton.GetComponent<Image>().sprite;
        _variantMenuButton.GetComponent<Image>().sprite = _variantMenuButtonSpriteSwap;
        _variantMenuButtonSpriteSwap = t;
        _variantMenuButton.enabled = true;
    }
}
