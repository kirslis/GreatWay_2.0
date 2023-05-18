using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackButtonScript : AbilityButton
{
    [SerializeField] AttackSettingsMenu _settingsMenu;

    private AtackButtonINput input;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        input = new AtackButtonINput();
        input.Mouse.RightMouse.performed += context =>
        {
            Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OpenSettings();
                Debug.Log("CLICK");
            }
        };

        input.Enable();
    }

    private void OpenSettings()
    {
        if (!_settingsMenu.isOpen)
            _settingsMenu.Interact();
    }

    public override void OnClick()
    {
        base.OnClick();
        if (IsAiming) { }
        else
            IsAiming = true;
    }

    public override void StopAiming()
    {
        base.StopAiming();
        IsAiming = false;
    }

}
