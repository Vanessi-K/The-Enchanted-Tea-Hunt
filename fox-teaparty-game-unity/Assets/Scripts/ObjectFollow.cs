using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    
    void Update()
    {
        Quaternion originalRotation = transform.rotation;
        transform.position = objectToFollow.position;
        transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, objectToFollow.rotation.eulerAngles.y, originalRotation.eulerAngles.z);
        
        transform.Translate(offset);
    }
}
