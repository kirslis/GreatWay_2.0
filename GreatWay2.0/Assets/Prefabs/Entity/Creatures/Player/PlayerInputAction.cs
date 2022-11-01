// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/Entity/Creatures/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""MoveActions"",
            ""id"": ""b546a766-de19-47a6-858e-510bab2586d5"",
            ""actions"": [
                {
                    ""name"": ""LookOut"",
                    ""type"": ""Button"",
                    ""id"": ""1c53df1b-61ad-4b11-9f16-4e056a7b97e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RefreshPath"",
                    ""type"": ""Button"",
                    ""id"": ""6e6fdf5c-0ff8-4047-ac82-b4b4566a55d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HoldToMove"",
                    ""type"": ""Button"",
                    ""id"": ""437e1722-495d-4649-b36d-3ade97701bcd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e99957cd-56fd-4643-b69a-6c69e39a9cf9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43675c28-68e8-4d26-b1ab-9b4792de8e97"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RefreshPath"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81cbfa1a-400b-49da-980b-d8ad8f191aa3"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RefreshPath"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""501e9018-6955-4229-b8eb-5690e90e12c5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HoldToMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Key and mouse"",
            ""bindingGroup"": ""Key and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MoveActions
        m_MoveActions = asset.FindActionMap("MoveActions", throwIfNotFound: true);
        m_MoveActions_LookOut = m_MoveActions.FindAction("LookOut", throwIfNotFound: true);
        m_MoveActions_RefreshPath = m_MoveActions.FindAction("RefreshPath", throwIfNotFound: true);
        m_MoveActions_HoldToMove = m_MoveActions.FindAction("HoldToMove", throwIfNotFound: true);
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

    // MoveActions
    private readonly InputActionMap m_MoveActions;
    private IMoveActionsActions m_MoveActionsActionsCallbackInterface;
    private readonly InputAction m_MoveActions_LookOut;
    private readonly InputAction m_MoveActions_RefreshPath;
    private readonly InputAction m_MoveActions_HoldToMove;
    public struct MoveActionsActions
    {
        private @PlayerInputAction m_Wrapper;
        public MoveActionsActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookOut => m_Wrapper.m_MoveActions_LookOut;
        public InputAction @RefreshPath => m_Wrapper.m_MoveActions_RefreshPath;
        public InputAction @HoldToMove => m_Wrapper.m_MoveActions_HoldToMove;
        public InputActionMap Get() { return m_Wrapper.m_MoveActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActionsActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActionsActions instance)
        {
            if (m_Wrapper.m_MoveActionsActionsCallbackInterface != null)
            {
                @LookOut.started -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnLookOut;
                @LookOut.performed -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnLookOut;
                @LookOut.canceled -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnLookOut;
                @RefreshPath.started -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnRefreshPath;
                @RefreshPath.performed -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnRefreshPath;
                @RefreshPath.canceled -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnRefreshPath;
                @HoldToMove.started -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnHoldToMove;
                @HoldToMove.performed -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnHoldToMove;
                @HoldToMove.canceled -= m_Wrapper.m_MoveActionsActionsCallbackInterface.OnHoldToMove;
            }
            m_Wrapper.m_MoveActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LookOut.started += instance.OnLookOut;
                @LookOut.performed += instance.OnLookOut;
                @LookOut.canceled += instance.OnLookOut;
                @RefreshPath.started += instance.OnRefreshPath;
                @RefreshPath.performed += instance.OnRefreshPath;
                @RefreshPath.canceled += instance.OnRefreshPath;
                @HoldToMove.started += instance.OnHoldToMove;
                @HoldToMove.performed += instance.OnHoldToMove;
                @HoldToMove.canceled += instance.OnHoldToMove;
            }
        }
    }
    public MoveActionsActions @MoveActions => new MoveActionsActions(this);
    private int m_KeyandmouseSchemeIndex = -1;
    public InputControlScheme KeyandmouseScheme
    {
        get
        {
            if (m_KeyandmouseSchemeIndex == -1) m_KeyandmouseSchemeIndex = asset.FindControlSchemeIndex("Key and mouse");
            return asset.controlSchemes[m_KeyandmouseSchemeIndex];
        }
    }
    public interface IMoveActionsActions
    {
        void OnLookOut(InputAction.CallbackContext context);
        void OnRefreshPath(InputAction.CallbackContext context);
        void OnHoldToMove(InputAction.CallbackContext context);
    }
}
