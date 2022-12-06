// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/AtackButtonINput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @AtackButtonINput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @AtackButtonINput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AtackButtonINput"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""0c596db2-3259-423c-a6e0-0fc66c2cd14d"",
            ""actions"": [
                {
                    ""name"": ""RightMouse"",
                    ""type"": ""Button"",
                    ""id"": ""23af403f-f76e-45ad-baeb-67017292fa30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""840f17a6-abad-4cb0-b4c8-adbf9ea334dd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_RightMouse = m_Mouse.FindAction("RightMouse", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_RightMouse;
    public struct MouseActions
    {
        private @AtackButtonINput m_Wrapper;
        public MouseActions(@AtackButtonINput wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightMouse => m_Wrapper.m_Mouse_RightMouse;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @RightMouse.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightMouse.started += instance.OnRightMouse;
                @RightMouse.performed += instance.OnRightMouse;
                @RightMouse.canceled += instance.OnRightMouse;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMouseActions
    {
        void OnRightMouse(InputAction.CallbackContext context);
    }
}
