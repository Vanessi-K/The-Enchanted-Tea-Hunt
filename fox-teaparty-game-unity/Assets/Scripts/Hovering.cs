using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float movementTime;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _passedMovementTime;
    private Quaternion _targetRotation;

    void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + movementDirection;
    }

    void Update()
    {
        if (_passedMovementTime >= movementTime)
        {
            ChangeMovementDirection();
        }

        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _passedMovementTime / movementTime);
        
        _passedMovementTime += Time.deltaTime;
    }

    void ChangeMovementDirection()
    {
        (_startPosition, _targetPosition) = (_targetPosition, _startPosition);
        _passedMovementTime = 0;
    }
}
