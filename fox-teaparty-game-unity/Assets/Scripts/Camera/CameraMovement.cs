using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float maximumAngle;
    [SerializeField] private float closestDistance;
    [SerializeField] private float farthestDistance;
    private float _zoomModifier;
    private float _lastZoomModifier;

    private void Update()
    {
        Quaternion originalRotation = transform.rotation;
        transform.position = objectToFollow.position;
        transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, objectToFollow.rotation.eulerAngles.y, originalRotation.eulerAngles.z);
        transform.Translate(offset);
        CameraZoom(_zoomModifier);
    }
    

    private void OnVerticalRotation(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        RotateVertically(inputVector.y);
    }
    
    private void OnZoom(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        _lastZoomModifier = inputVector.y * 0.05f;
        _zoomModifier += _lastZoomModifier;
    }
    
    private void CameraZoom(float zoomDistance)
    {
        transform.position += (transform.forward * zoomDistance);
        
        float distance = Mathf.Abs(Vector3.Distance(transform.position, objectToFollow.position));
        if(distance < closestDistance || distance > farthestDistance)
        {
            transform.position -= transform.forward * _lastZoomModifier;
            _zoomModifier -= _lastZoomModifier;
            _lastZoomModifier = 0;
        }
    }
    
    void RotateVertically(float angle)
    {
        Vector3 originalPosition = transform.position;
        Quaternion originalRotation = this.transform.rotation;
        transform.RotateAround(objectToFollow.position, transform.right, angle);
        
        if(Vector3.Angle(objectToFollow.forward, this.transform.forward) > maximumAngle)
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
