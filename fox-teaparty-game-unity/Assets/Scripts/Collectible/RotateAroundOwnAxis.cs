using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundOwnAxis : MonoBehaviour
{
    [SerializeField] private float rotationAngle;
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationAngle);
    }
}
