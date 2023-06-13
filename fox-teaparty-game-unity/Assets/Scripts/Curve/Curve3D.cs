using UnityEngine;

public class Curve3D : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints;
    private Vector3 _gizmoPosition;
    
    void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            _gizmoPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1-t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(_gizmoPosition, 0.25f);
        }
        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }
}
