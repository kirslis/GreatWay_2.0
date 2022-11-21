// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/ActionButtonActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ActionButtonActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ActionButtonActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ActionButtonActions"",
    ""maps"": [
        {
            ""name"": ""Chosen"",
            ""id"": ""ea5b0b96-1d58-4cdc-85d4-8c700c3dabe7"",
            ""actions"": [
                {
                    ""name"": ""mouseLeft"",
                    ""type"": ""Button"",
                    ""id"": ""0126ee64-fc57-4a71-8891-00a2beba8c0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""mouseRight"",
                    ""type"": ""Button"",
                    ""id"": ""3020dac1-a95c-4365-b18e-e86d32809c8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be535d21-2d9c-403c-845d-0c370943358c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mouseLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4843a10a-6c4f-446b-8cda-bc99c1d63410"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mouseRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Chosen
        m_Chosen = asset.FindActionMap("Chosen", throwIfNotFound: true);
        m_Chosen_mouseLeft = m_Chosen.FindAction("mouseLeft", throwIfNotFound: true);
        m_Chosen_mouseRight = m_Chosen.FindAction("mouseRight", throwIfNotFound: true);
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

    // Chosen
    private readonly InputActionMap m_Chosen;
    private IChosenActions m_ChosenActionsCallbackInterface;
    private readonly InputAction m_Chosen_mouseLeft;
    private readonly InputAction m_Chosen_mouseRight;
    public struct ChosenActions
    {
        private @ActionButtonActions m_Wrapper;
        public ChosenActions(@ActionButtonActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @mouseLeft => m_Wrapper.m_Chosen_mouseLeft;
        public InputAction @mouseRight => m_Wrapper.m_Chosen_mouseRight;
        public InputActionMap Get() { return m_Wrapper.m_Chosen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChosenActions set) { return set.Get(); }
        public void SetCallbacks(IChosenActions instance)
        {
            if (m_Wrapper.m_ChosenActionsCallbackInterface != null)
            {
                @mouseLeft.started -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseLeft;
                @mouseLeft.performed -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseLeft;
                @mouseLeft.canceled -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseLeft;
                @mouseRight.started -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseRight;
                @mouseRight.performed -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseRight;
                @mouseRight.canceled -= m_Wrapper.m_ChosenActionsCallbackInterface.OnMouseRight;
            }
            m_Wrapper.m_ChosenActionsCallbackInterface = instance;
            if (instance != null)
            {
                @mouseLeft.started += instance.OnMouseLeft;
                @mouseLeft.performed += instance.OnMouseLeft;
                @mouseLeft.canceled += instance.OnMouseLeft;
                @mouseRight.started += instance.OnMouseRight;
                @mouseRight.performed += instance.OnMouseRight;
                @mouseRight.canceled += instance.OnMouseRight;
            }
        }
    }
    public ChosenActions @Chosen => new ChosenActions(this);
    public interface IChosenActions
    {
        void OnMouseLeft(InputAction.CallbackContext context);
        void OnMouseRight(InputAction.CallbackContext context);
    }
}
