namespace VUDK.Features.Main.CharacterController.Movements
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Serializable;

    public class FirstPersonMovement : CharacterController3DBase
    {
        private void OnEnable()
        {
            InputsManager.Inputs.Player.Movement.canceled += StopInput;
            InputsManager.Inputs.Player.Jump.performed += JumpInput;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Player.Movement.canceled -= StopInput;
            InputsManager.Inputs.Player.Jump.performed -= JumpInput;
        }

        protected override void Update()
        {
            base.Update();
            MoveInput();
        }

        private void MoveInput()
        {
            InputAction movement = InputsManager.Inputs.Player.Movement;
            if (movement.IsInProgress())
                MoveCharacter(movement.ReadValue<Vector2>());
        }

        private void StopInput(InputAction.CallbackContext context)
        {
            StopCharacterPositionOnAxes(new XYZBools(x: true, y: false, z: true));
        }

        private void JumpInput(InputAction.CallbackContext context)
        {
            Jump(Vector3.up);
        }
    }
}
