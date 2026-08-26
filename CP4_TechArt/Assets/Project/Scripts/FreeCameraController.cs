using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class FreeCameraController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fastSpeed = 15f;
    [SerializeField] private float slowSpeed = 2f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.15f;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Vector3 rotation = transform.eulerAngles;

        rotationY = rotation.y;
        rotationX = rotation.x;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    private void HandleMovement()
    {
        if (Keyboard.current == null)
            return;

        Vector3 direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            direction += transform.forward;

        if (Keyboard.current.sKey.isPressed)
            direction -= transform.forward;

        if (Keyboard.current.dKey.isPressed)
            direction += transform.right;

        if (Keyboard.current.aKey.isPressed)
            direction -= transform.right;

        if (Keyboard.current.eKey.isPressed)
            direction += Vector3.up;

        if (Keyboard.current.qKey.isPressed)
            direction -= Vector3.up;

        float currentSpeed = moveSpeed;

        if (Keyboard.current.leftShiftKey.isPressed)
            currentSpeed = fastSpeed;

        if (Keyboard.current.leftCtrlKey.isPressed)
            currentSpeed = slowSpeed;

        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
    }

    private void HandleLook()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.rightButton.isPressed)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        rotationY += mouseDelta.x * mouseSensitivity;
        rotationX -= mouseDelta.y * mouseSensitivity;

        rotationX = Mathf.Clamp(rotationX, -89f, 89f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
    
}