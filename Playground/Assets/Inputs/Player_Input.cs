// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Player_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player_Input"",
    ""maps"": [
        {
            ""name"": ""CharacterControl"",
            ""id"": ""b00a1b8d-93c4-47bd-8504-45a06fd2dae7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e47021d9-5854-42ee-9388-3f9ab5643786"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""7e55f43a-87ce-4ed8-b75d-b4d91c82a5ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackMode"",
                    ""type"": ""Button"",
                    ""id"": ""b9ea84e8-f4f3-48da-b65a-fa863c0473cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""98229c21-ff64-43e6-a465-aff02c4fdfc9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2635f475-fe3c-4689-8700-0fa57cfb1d25"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d60e19e-43ae-413e-9a49-ae7fbc4d4e49"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adec95a2-7962-4f8f-87ea-1a7c436232b3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b418488-3211-40e0-829b-01c0807de514"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControl
        m_CharacterControl = asset.FindActionMap("CharacterControl", throwIfNotFound: true);
        m_CharacterControl_Movement = m_CharacterControl.FindAction("Movement", throwIfNotFound: true);
        m_CharacterControl_Run = m_CharacterControl.FindAction("Run", throwIfNotFound: true);
        m_CharacterControl_AttackMode = m_CharacterControl.FindAction("AttackMode", throwIfNotFound: true);
        m_CharacterControl_Attack = m_CharacterControl.FindAction("Attack", throwIfNotFound: true);
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

    // CharacterControl
    private readonly InputActionMap m_CharacterControl;
    private ICharacterControlActions m_CharacterControlActionsCallbackInterface;
    private readonly InputAction m_CharacterControl_Movement;
    private readonly InputAction m_CharacterControl_Run;
    private readonly InputAction m_CharacterControl_AttackMode;
    private readonly InputAction m_CharacterControl_Attack;
    public struct CharacterControlActions
    {
        private @Player_Input m_Wrapper;
        public CharacterControlActions(@Player_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterControl_Movement;
        public InputAction @Run => m_Wrapper.m_CharacterControl_Run;
        public InputAction @AttackMode => m_Wrapper.m_CharacterControl_AttackMode;
        public InputAction @Attack => m_Wrapper.m_CharacterControl_Attack;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlActions instance)
        {
            if (m_Wrapper.m_CharacterControlActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMovement;
                @Run.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRun;
                @AttackMode.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttackMode;
                @AttackMode.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttackMode;
                @AttackMode.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttackMode;
                @Attack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CharacterControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @AttackMode.started += instance.OnAttackMode;
                @AttackMode.performed += instance.OnAttackMode;
                @AttackMode.canceled += instance.OnAttackMode;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CharacterControlActions @CharacterControl => new CharacterControlActions(this);
    public interface ICharacterControlActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnAttackMode(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
