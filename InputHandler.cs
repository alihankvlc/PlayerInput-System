using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.KVLC
{
    public class InputHandler : MonoBehaviour
    {
        private InputActionMap _actionMap;

        private InputAction _moveInputAction;
        private InputAction _walkInputAction;
        private InputAction _sprintInputAction;
        private InputAction _lookInputAction;

        private const string MOVE_INPUT_ENTRY = "Move";
        private const string WALK_INPUT_ENTRY = "Walk";
        private const string SPRINT_INPUT_ENTRY = "Sprint";
        private const string LOOK_INPUT_ENTRY = "Look";

        [HideInInspector] public PlayerInput PlayerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        public bool Walk { get; private set; }
        public bool Sprint { get; private set; }

        private void Awake()
        {
            if (PlayerInput == null)
            {
                return;
            }

            _actionMap = PlayerInput.currentActionMap;

            _moveInputAction = _actionMap.FindAction(MOVE_INPUT_ENTRY);
            _walkInputAction = _actionMap.FindAction(WALK_INPUT_ENTRY);
            _sprintInputAction = _actionMap.FindAction(SPRINT_INPUT_ENTRY);
            _lookInputAction = _actionMap.FindAction(LOOK_INPUT_ENTRY);

        }

        private void OnEnable()
        {
          
                _actionMap.Enable();
                SubscribeContext();
        }

        private void SubscribeContext()
        {
            _moveInputAction.performed += context => Move = context.ReadValue<Vector2>();
            _moveInputAction.canceled += context => Move = context.ReadValue<Vector2>();

            _lookInputAction.performed += context => Look = context.ReadValue<Vector2>();
            _lookInputAction.canceled += context => Look = context.ReadValue<Vector2>();

            _walkInputAction.performed += context => Walk = context.ReadValueAsButton();
            _walkInputAction.canceled += context => Walk = context.ReadValueAsButton();

            _sprintInputAction.performed += context => Sprint = context.ReadValueAsButton();
            _sprintInputAction.canceled += context => Sprint = context.ReadValueAsButton();
        }

        private void UnsubscribeContext()
        {


            _moveInputAction.performed -= context => Move = context.ReadValue<Vector2>();
            _moveInputAction.canceled -= context => Move = context.ReadValue<Vector2>();

            _lookInputAction.performed -= context => Look = context.ReadValue<Vector2>();
            _lookInputAction.canceled -= context => Look = context.ReadValue<Vector2>();



            _walkInputAction.performed -= context => Walk = context.ReadValueAsButton();
            _walkInputAction.canceled -= context => Walk = context.ReadValueAsButton();


            _sprintInputAction.performed -= context => Sprint = context.ReadValueAsButton();
            _sprintInputAction.canceled -= context => Sprint = context.ReadValueAsButton();
        }

        private void OnDisable()
        {

            _actionMap.Disable();
        }

        private void OnDestroy()
        {
            UnsubscribeContext();
        }
    }
}
