// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/AttackPanelKInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class AttackPanelInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public AttackPanelInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AttackPanelKInput"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""1c31ec76-2b82-4fe9-93ef-f5e8282efee1"",
            ""actions"": [
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""7f2729fc-0f2e-40ad-a2a4-bc18c68df12a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b63708d5-1386-4b54-852e-3e66d7b5f48e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
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
        m_Mouse_RightClick = m_Mouse.FindAction("RightClick", throwIfNotFound: true);
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
    private readonly InputAction m_Mouse_RightClick;
    public struct MouseActions
    {
        private AttackPanelInput m_Wrapper;
        public MouseActions(AttackPanelInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightClick => m_Wrapper.m_Mouse_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @RightClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMouseActions
    {
        void OnRightClick(InputAction.CallbackContext context);
    }
}
