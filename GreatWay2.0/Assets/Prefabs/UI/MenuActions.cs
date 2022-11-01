// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/MenuActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MenuActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuActions"",
    ""maps"": [
        {
            ""name"": ""MenuesAction"",
            ""id"": ""127b75f9-47c4-422c-b778-f05fa45555dc"",
            ""actions"": [
                {
                    ""name"": ""OpenParent"",
                    ""type"": ""Button"",
                    ""id"": ""7db452bf-df5b-4cee-87bf-f199c3f7f682"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f375f8f4-9d71-4eef-9536-f16b0b6590b7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenParent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MenuesAction
        m_MenuesAction = asset.FindActionMap("MenuesAction", throwIfNotFound: true);
        m_MenuesAction_OpenParent = m_MenuesAction.FindAction("OpenParent", throwIfNotFound: true);
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

    // MenuesAction
    private readonly InputActionMap m_MenuesAction;
    private IMenuesActionActions m_MenuesActionActionsCallbackInterface;
    private readonly InputAction m_MenuesAction_OpenParent;
    public struct MenuesActionActions
    {
        private @MenuActions m_Wrapper;
        public MenuesActionActions(@MenuActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenParent => m_Wrapper.m_MenuesAction_OpenParent;
        public InputActionMap Get() { return m_Wrapper.m_MenuesAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuesActionActions set) { return set.Get(); }
        public void SetCallbacks(IMenuesActionActions instance)
        {
            if (m_Wrapper.m_MenuesActionActionsCallbackInterface != null)
            {
                @OpenParent.started -= m_Wrapper.m_MenuesActionActionsCallbackInterface.OnOpenParent;
                @OpenParent.performed -= m_Wrapper.m_MenuesActionActionsCallbackInterface.OnOpenParent;
                @OpenParent.canceled -= m_Wrapper.m_MenuesActionActionsCallbackInterface.OnOpenParent;
            }
            m_Wrapper.m_MenuesActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenParent.started += instance.OnOpenParent;
                @OpenParent.performed += instance.OnOpenParent;
                @OpenParent.canceled += instance.OnOpenParent;
            }
        }
    }
    public MenuesActionActions @MenuesAction => new MenuesActionActions(this);
    public interface IMenuesActionActions
    {
        void OnOpenParent(InputAction.CallbackContext context);
    }
}
