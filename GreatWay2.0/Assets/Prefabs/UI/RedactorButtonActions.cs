// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/UI/RedactorButtonActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RedactorButtonActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RedactorButtonActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RedactorButtonActions"",
    ""maps"": [
        {
            ""name"": ""Reduct"",
            ""id"": ""6188b003-3f17-47ce-b084-812bbfaab260"",
            ""actions"": [
                {
                    ""name"": ""AbortReduct"",
                    ""type"": ""Button"",
                    ""id"": ""275b5680-d548-4577-89f5-f60ac7519a0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReductMap"",
                    ""type"": ""Button"",
                    ""id"": ""d6d581ab-eed6-47ef-a4ab-239ca9f694fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f047087d-8419-4524-b55d-6f9bc90812c1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbortReduct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f633108a-c87d-4098-bc4e-22cf8cc79e72"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""AbortReduct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8282cbe1-83e0-4fa3-9fc6-12af2d885c61"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""ReductMap"",
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
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Reduct
        m_Reduct = asset.FindActionMap("Reduct", throwIfNotFound: true);
        m_Reduct_AbortReduct = m_Reduct.FindAction("AbortReduct", throwIfNotFound: true);
        m_Reduct_ReductMap = m_Reduct.FindAction("ReductMap", throwIfNotFound: true);
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

    // Reduct
    private readonly InputActionMap m_Reduct;
    private IReductActions m_ReductActionsCallbackInterface;
    private readonly InputAction m_Reduct_AbortReduct;
    private readonly InputAction m_Reduct_ReductMap;
    public struct ReductActions
    {
        private @RedactorButtonActions m_Wrapper;
        public ReductActions(@RedactorButtonActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @AbortReduct => m_Wrapper.m_Reduct_AbortReduct;
        public InputAction @ReductMap => m_Wrapper.m_Reduct_ReductMap;
        public InputActionMap Get() { return m_Wrapper.m_Reduct; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ReductActions set) { return set.Get(); }
        public void SetCallbacks(IReductActions instance)
        {
            if (m_Wrapper.m_ReductActionsCallbackInterface != null)
            {
                @AbortReduct.started -= m_Wrapper.m_ReductActionsCallbackInterface.OnAbortReduct;
                @AbortReduct.performed -= m_Wrapper.m_ReductActionsCallbackInterface.OnAbortReduct;
                @AbortReduct.canceled -= m_Wrapper.m_ReductActionsCallbackInterface.OnAbortReduct;
                @ReductMap.started -= m_Wrapper.m_ReductActionsCallbackInterface.OnReductMap;
                @ReductMap.performed -= m_Wrapper.m_ReductActionsCallbackInterface.OnReductMap;
                @ReductMap.canceled -= m_Wrapper.m_ReductActionsCallbackInterface.OnReductMap;
            }
            m_Wrapper.m_ReductActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AbortReduct.started += instance.OnAbortReduct;
                @AbortReduct.performed += instance.OnAbortReduct;
                @AbortReduct.canceled += instance.OnAbortReduct;
                @ReductMap.started += instance.OnReductMap;
                @ReductMap.performed += instance.OnReductMap;
                @ReductMap.canceled += instance.OnReductMap;
            }
        }
    }
    public ReductActions @Reduct => new ReductActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IReductActions
    {
        void OnAbortReduct(InputAction.CallbackContext context);
        void OnReductMap(InputAction.CallbackContext context);
    }
}
