// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/DiceActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DiceActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DiceActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DiceActions"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""fef008d8-7c29-4fa9-a27f-500e80863374"",
            ""actions"": [
                {
                    ""name"": ""ActivateDice"",
                    ""type"": ""Button"",
                    ""id"": ""1f42e101-5d33-44ce-ae59-649fa6f657f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc2d1fd8-8f93-4eb5-bf2d-787125abe3ad"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateDice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_ActivateDice = m_Mouse.FindAction("ActivateDice", throwIfNotFound: true);
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
    private readonly InputAction m_Mouse_ActivateDice;
    public struct MouseActions
    {
        private @DiceActions m_Wrapper;
        public MouseActions(@DiceActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActivateDice => m_Wrapper.m_Mouse_ActivateDice;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @ActivateDice.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnActivateDice;
                @ActivateDice.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnActivateDice;
                @ActivateDice.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnActivateDice;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ActivateDice.started += instance.OnActivateDice;
                @ActivateDice.performed += instance.OnActivateDice;
                @ActivateDice.canceled += instance.OnActivateDice;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnActivateDice(InputAction.CallbackContext context);
    }
}
