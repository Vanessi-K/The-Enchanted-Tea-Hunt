using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private bool _isJumpingOrFalling;
    private bool _walkingPressed;
    private Backpack _backpack;
    [SerializeField] private GameObject boostEffect;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _decreaseValuePerWeightUnit;
    [SerializeField] private Animator animator;

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
            transform.Translate(Vector3.forward * (Time.deltaTime * _speed * weightModifier * _backpack.SpeedBoost));
        }
        
        Debug.Log("Jump: " + _isJumpingOrFalling + "; Walk: " + _walkingPressed);
        // animator.SetBool("walking", _walkingPressed);
        // animator.SetBool("jumping", _isJumpingOrFalling);
        
        if(animator.GetBool("walking") != _walkingPressed)
        {
            animator.SetBool("walking", _walkingPressed);
        }
        
        if(animator.GetBool("jumping") != _isJumpingOrFalling)
        {
            animator.SetBool("jumping", _isJumpingOrFalling);
        }
        
        if(_backpack.SpeedBoost > 1)
        {
            boostEffect.SetActive(true);
        }
        else
        {
            boostEffect.SetActive(false);
            AkSoundEngine.PostEvent("Stop_activate_berry", gameObject);
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
        AkSoundEngine.PostEvent("Play_jump", gameObject);
    }

    private void OnActivateBoost(InputValue inputValue)
    {
        _backpack.UseSpeedBoost();
        AkSoundEngine.PostEvent("Play_activate_berry", gameObject);
    }

    private void RotateHorizontal(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
    
}
