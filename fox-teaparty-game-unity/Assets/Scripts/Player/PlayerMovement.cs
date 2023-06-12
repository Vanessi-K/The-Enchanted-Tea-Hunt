using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool _isJumpingOrFalling;
    private bool _isWalking;
    private bool _walkingPressed;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private void Update()
    {
        _isJumpingOrFalling = GetComponent<Rigidbody>().velocity.y < -.035 || GetComponent<Rigidbody>().velocity.y > 0.00001;
        if (_walkingPressed)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * _speed));
        }
    }

    private void OnMovement(InputValue inputValue)
    {
        _walkingPressed = inputValue.isPressed;
    }

    private void OnRotation(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        RotateHorizontal(inputVector.x);
    }
    
    private void OnJump(InputValue inputValue)
    {
        if (_isJumpingOrFalling) return;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    private void RotateHorizontal(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
}
