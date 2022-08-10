//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/Settings/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""e39c0baa-67b3-4911-bff5-6bbac8f2d26c"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Value"",
                    ""id"": ""5917ab02-db85-4395-b4f1-331e82443933"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4702415e-dd2f-4467-a77e-2c3763109ce2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b2da674f-dd73-4b06-875a-1407729a2112"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be4ab43d-827b-49e0-ad8b-3ab63b63518c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e9bfdf8-51c2-40eb-ba0c-99f84cfb42fe"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7751fdb7-1e84-4b02-8b70-e3f697e9072e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keys"",
            ""id"": ""1185c3f8-7465-4af6-834f-4ed775b65cd1"",
            ""actions"": [
                {
                    ""name"": ""Undo"",
                    ""type"": ""Button"",
                    ""id"": ""ef640f94-df21-47b5-8dc1-39332de32ef8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""f43456b7-b627-4e07-bf8b-4b6c44d2c4e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91db4f5b-01e6-41ab-aca3-75f9815e1edf"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Undo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce1806ad-318f-4aa2-8d92-316424629ed8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Click = m_Mouse.FindAction("Click", throwIfNotFound: true);
        m_Mouse_Move = m_Mouse.FindAction("Move", throwIfNotFound: true);
        m_Mouse_Delta = m_Mouse.FindAction("Delta", throwIfNotFound: true);
        // Keys
        m_Keys = asset.FindActionMap("Keys", throwIfNotFound: true);
        m_Keys_Undo = m_Keys.FindAction("Undo", throwIfNotFound: true);
        m_Keys_Reset = m_Keys.FindAction("Reset", throwIfNotFound: true);
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

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_Click;
    private readonly InputAction m_Mouse_Move;
    private readonly InputAction m_Mouse_Delta;
    public struct MouseActions
    {
        private @PlayerInput m_Wrapper;
        public MouseActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Mouse_Click;
        public InputAction @Move => m_Wrapper.m_Mouse_Move;
        public InputAction @Delta => m_Wrapper.m_Mouse_Delta;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Move.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Delta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
                @Delta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
                @Delta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Delta.started += instance.OnDelta;
                @Delta.performed += instance.OnDelta;
                @Delta.canceled += instance.OnDelta;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // Keys
    private readonly InputActionMap m_Keys;
    private IKeysActions m_KeysActionsCallbackInterface;
    private readonly InputAction m_Keys_Undo;
    private readonly InputAction m_Keys_Reset;
    public struct KeysActions
    {
        private @PlayerInput m_Wrapper;
        public KeysActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Undo => m_Wrapper.m_Keys_Undo;
        public InputAction @Reset => m_Wrapper.m_Keys_Reset;
        public InputActionMap Get() { return m_Wrapper.m_Keys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeysActions set) { return set.Get(); }
        public void SetCallbacks(IKeysActions instance)
        {
            if (m_Wrapper.m_KeysActionsCallbackInterface != null)
            {
                @Undo.started -= m_Wrapper.m_KeysActionsCallbackInterface.OnUndo;
                @Undo.performed -= m_Wrapper.m_KeysActionsCallbackInterface.OnUndo;
                @Undo.canceled -= m_Wrapper.m_KeysActionsCallbackInterface.OnUndo;
                @Reset.started -= m_Wrapper.m_KeysActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_KeysActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_KeysActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_KeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Undo.started += instance.OnUndo;
                @Undo.performed += instance.OnUndo;
                @Undo.canceled += instance.OnUndo;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
            }
        }
    }
    public KeysActions @Keys => new KeysActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
    }
    public interface IKeysActions
    {
        void OnUndo(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
