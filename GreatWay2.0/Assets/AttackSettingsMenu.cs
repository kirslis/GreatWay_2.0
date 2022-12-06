using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AttackSettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _buttonsContainer;

    private AttackPanelInput Action;
    private Camera Cam;
    private bool IsOpen = false;
    private BoxCollider collider;

    public bool isOpen { get { return IsOpen; } }
    public GameObject buttonContainer { get { return _buttonsContainer; } }

    private void Awake()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        collider = GetComponent<BoxCollider>();

        Action = new AttackPanelInput();
        Action.Mouse.RightClick.performed += context =>
        {
            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);
            if (hit.collider == null || (hit.collider.gameObject != transform.parent.gameObject && hit.collider.gameObject != gameObject))
            {
                Close();
            }
        };

        Action.Enable();
    }

    public void Interact()
    {
        if (!IsOpen)
        {
            StartCoroutine(ChangeSizeVarianMenuCoroutine(
                 (_buttonsContainer.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + _buttonsContainer.GetComponent<GridLayoutGroup>().spacing.y) * buttonContainer.transform.childCount
                 ));
            Action.Enable();
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        StartCoroutine(ChangeSizeVarianMenuCoroutine(0));
        Action.Disable();
    }

    IEnumerator ChangeSizeVarianMenuCoroutine(float targetHeight)
    {
        float speed = 300f;
        RectTransform Rect = _panel.GetComponent<RectTransform>();

        while (Rect.sizeDelta.y != targetHeight)
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, new Vector2(Rect.sizeDelta.x, targetHeight), speed * Time.deltaTime);
            collider.size = Rect.sizeDelta;
            collider.center = new Vector2(0, collider.size.y / 2);
            yield return null;
        }

        IsOpen = !IsOpen;
    }

}
