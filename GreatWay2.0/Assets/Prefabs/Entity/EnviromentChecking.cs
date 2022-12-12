//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Prefabs/Entity/EnviromentChecking.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @EnviromentChecking : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @EnviromentChecking()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""EnviromentChecking"",
    ""maps"": [
        {
            ""name"": ""CheckEnviroment"",
            ""id"": ""378db83d-4eba-4c64-a089-12e34e195d8c"",
            ""actions"": [
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""17fd270d-190f-40bd-bd65-33497aa2e338"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a488a0cc-e795-45a0-8de9-db135362c839"",
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
        // CheckEnviroment
        m_CheckEnviroment = asset.FindActionMap("CheckEnviroment", throwIfNotFound: true);
        m_CheckEnviroment_RightClick = m_CheckEnviroment.FindAction("RightClick", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CheckEnviroment
    private readonly InputActionMap m_CheckEnviroment;
    private ICheckEnviromentActions m_CheckEnviromentActionsCallbackInterface;
    private readonly InputAction m_CheckEnviroment_RightClick;
    public struct CheckEnviromentActions
    {
        private @EnviromentChecking m_Wrapper;
        public CheckEnviromentActions(@EnviromentChecking wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightClick => m_Wrapper.m_CheckEnviroment_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_CheckEnviroment; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheckEnviromentActions set) { return set.Get(); }
        public void SetCallbacks(ICheckEnviromentActions instance)
        {
            if (m_Wrapper.m_CheckEnviromentActionsCallbackInterface != null)
            {
                @RightClick.started -= m_Wrapper.m_CheckEnviromentActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_CheckEnviromentActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_CheckEnviromentActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_CheckEnviromentActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public CheckEnviromentActions @CheckEnviroment => new CheckEnviromentActions(this);
    public interface ICheckEnviromentActions
    {
        void OnRightClick(InputAction.CallbackContext context);
    }
}