using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private bool _isJumpingOrFalling;
    private bool _isWalking;
    private bool _walkingPressed;
    private Backpack _backpack;
    [SerializeField] private GameObject boostEffect;
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
            transform.Translate(Vector3.forward * (Time.deltaTime * _speed * weightModifier * _backpack.SpeedBoost));
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
    
    // check for collision with layer solid
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            AkSoundEngine.PostEvent("Play_wall_hit", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            AkSoundEngine.PostEvent("Play_bush_hit", gameObject);
        }
    }
}
