using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private bool _isJumpingOrFalling;
    private bool _isWalking;
    private bool _walkingPressed;
    private Backpack _backpack;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _decreaseValuePerWeightUnit;

    private void Start()
    {
        _backpack = GameStats.Instance.Backpack;
    }
    
    private void Update()
    {
        _isJumpingOrFalling = GetComponent<Rigidbody>().velocity.y < -.035 || GetComponent<Rigidbody>().velocity.y > 0.00001;
        if (_walkingPressed)
        {
            float weightModifier = 1 - (_backpack.TotalWeight * _decreaseValuePerWeightUnit);
            transform.Translate(Vector3.forward * (Time.deltaTime * _speed * weightModifier));
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
        float weightModifier = 1 - (_backpack.TotalWeight * _decreaseValuePerWeightUnit);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5 * weightModifier, ForceMode.Impulse);
    }

    private void RotateHorizontal(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
}
