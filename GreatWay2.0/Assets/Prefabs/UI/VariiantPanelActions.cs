// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/VariiantPanelActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @VariiantPanelActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @VariiantPanelActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""VariiantPanelActions"",
    ""maps"": [
        {
            ""name"": ""Opened"",
            ""id"": ""dec07fb3-781f-492f-9a3f-90b97d031319"",
            ""actions"": [
                {
                    ""name"": ""LMouse"",
                    ""type"": ""Button"",
                    ""id"": ""e57e7800-c959-40e7-bcce-d7fa20b2b062"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e393e98a-abf7-4942-a4a5-29b90e951e4f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Opened
        m_Opened = asset.FindActionMap("Opened", throwIfNotFound: true);
        m_Opened_LMouse = m_Opened.FindAction("LMouse", throwIfNotFound: true);
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

    // Opened
    private readonly InputActionMap m_Opened;
    private IOpenedActions m_OpenedActionsCallbackInterface;
    private readonly InputAction m_Opened_LMouse;
    public struct OpenedActions
    {
        private @VariiantPanelActions m_Wrapper;
        public OpenedActions(@VariiantPanelActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @LMouse => m_Wrapper.m_Opened_LMouse;
        public InputActionMap Get() { return m_Wrapper.m_Opened; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OpenedActions set) { return set.Get(); }
        public void SetCallbacks(IOpenedActions instance)
        {
            if (m_Wrapper.m_OpenedActionsCallbackInterface != null)
            {
                @LMouse.started -= m_Wrapper.m_OpenedActionsCallbackInterface.OnLMouse;
                @LMouse.performed -= m_Wrapper.m_OpenedActionsCallbackInterface.OnLMouse;
                @LMouse.canceled -= m_Wrapper.m_OpenedActionsCallbackInterface.OnLMouse;
            }
            m_Wrapper.m_OpenedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LMouse.started += instance.OnLMouse;
                @LMouse.performed += instance.OnLMouse;
                @LMouse.canceled += instance.OnLMouse;
            }
        }
    }
    public OpenedActions @Opened => new OpenedActions(this);
    public interface IOpenedActions
    {
        void OnLMouse(InputAction.CallbackContext context);
    }
}
