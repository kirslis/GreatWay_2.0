// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/Abilities/AbilityInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @AbilityInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @AbilityInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AbilityInputs"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""c38530cd-1ba8-4c44-beb8-90838f6e2e79"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""3ca8bb87-c35e-48cb-85a4-b79a20688a2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbortAiming"",
                    ""type"": ""Button"",
                    ""id"": ""03448ca5-572d-40a8-af48-dc128302ad91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5939b8a6-d811-484b-9e2b-b0324dd61b4e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fafcc464-e3c0-4613-b888-f00359ecf86e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbortAiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca8e1b03-f1c2-4457-883d-9fb914141221"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbortAiming"",
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
        m_Mouse_Use = m_Mouse.FindAction("Use", throwIfNotFound: true);
        m_Mouse_AbortAiming = m_Mouse.FindAction("AbortAiming", throwIfNotFound: true);
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
    private readonly InputAction m_Mouse_Use;
    private readonly InputAction m_Mouse_AbortAiming;
    public struct MouseActions
    {
        private @AbilityInputs m_Wrapper;
        public MouseActions(@AbilityInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Mouse_Use;
        public InputAction @AbortAiming => m_Wrapper.m_Mouse_AbortAiming;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnUse;
                @AbortAiming.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnAbortAiming;
                @AbortAiming.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnAbortAiming;
                @AbortAiming.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnAbortAiming;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @AbortAiming.started += instance.OnAbortAiming;
                @AbortAiming.performed += instance.OnAbortAiming;
                @AbortAiming.canceled += instance.OnAbortAiming;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMouseActions
    {
        void OnUse(InputAction.CallbackContext context);
        void OnAbortAiming(InputAction.CallbackContext context);
    }
}
