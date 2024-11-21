using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMovement : MonoBehaviour
{
    // Example: var Accept = new InputAction(binding: "/<XRController>{leftHand}/{PrimaryButton}")

    // Variables
    public InputActionAsset inputActions;
    private InputAction joystickActionR;
    private InputAction joystickActionL;

    public Transform vrCamera;

    const float JOYSTICK_DEADZONE_THRESHOLD = 0.25f;
    const float PLAYER_MOVEMENT_ELEVATION_SPEED = 5.0f;
    const float PLAYER_MOVEMENT_STRAFE_SPEED = 5.0f;

    private void Awake()
    {
        // Load the InputActionAsset from Resources or a specific file path if needed
        inputActions = Resources.Load<InputActionAsset>("XRControls"); // Ensure XRControls is in a "Resources" folder

        if (inputActions != null)
        {
            // Locate the "Joystick" action in the "XRControllers" action map
            joystickActionR = inputActions.FindActionMap("XRControllers").FindAction("JoystickR");
            joystickActionL = inputActions.FindActionMap("XRControllers").FindAction("JoystickL");


            if (joystickActionR != null)
            {
                joystickActionR.Enable(); // Enable the action
            }
            if (joystickActionL != null)
            {
                joystickActionL.Enable(); // Enable the action
            }
            else
            {
                Debug.LogError("Joystick action not found.");
            }
        }
        else
        {
            Debug.LogError("InputActionAsset 'XRControls' not found in Resources folder.");
        }
    }
    private void OnDisable()
    {
        if (joystickActionR != null)
        {
            joystickActionR.Disable();
        }
    }

    void Update()
    {
        if (joystickActionR != null)
        {
            Vector2 joystickRValue = joystickActionR.ReadValue<Vector2>();
            ElevateMovement(joystickRValue);
        }

        if (joystickActionL != null)
        {
            Vector2 joystickLValue = joystickActionL.ReadValue<Vector2>();
            StrafeMovement(joystickLValue);
        }
    }

    void ElevateMovement(Vector2 right_Joystick_Value)
    {
        if (right_Joystick_Value.y > JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(Vector3.up * PLAYER_MOVEMENT_ELEVATION_SPEED * Time.deltaTime);
        }

        else if (right_Joystick_Value.y < -JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(Vector3.down * PLAYER_MOVEMENT_ELEVATION_SPEED * Time.deltaTime);
        }
    }

    void StrafeMovement(Vector2 left_Joystick_Value)
    {
        Vector3 forwardDirection = vrCamera.forward;
        Vector3 rightDirection = vrCamera.right;
        forwardDirection.y = 0;
        forwardDirection.Normalize();
        rightDirection.y = 0;
        rightDirection.Normalize();

        if (left_Joystick_Value.y > JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(forwardDirection * PLAYER_MOVEMENT_STRAFE_SPEED * Time.deltaTime * (1 + left_Joystick_Value.y * 10), Space.World);
        }

        else if (left_Joystick_Value.y < -JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(-forwardDirection * PLAYER_MOVEMENT_STRAFE_SPEED * Time.deltaTime * (1 + -left_Joystick_Value.y * 10), Space.World);
        }

        if (left_Joystick_Value.x > JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(rightDirection * PLAYER_MOVEMENT_STRAFE_SPEED * Time.deltaTime * (1 + left_Joystick_Value.x * 10), Space.World);
        }

        else if (left_Joystick_Value.x < -JOYSTICK_DEADZONE_THRESHOLD)
        {
            transform.Translate(-rightDirection * PLAYER_MOVEMENT_STRAFE_SPEED * Time.deltaTime * (1 + -left_Joystick_Value.x *10), Space.World);
        }
    }
}
