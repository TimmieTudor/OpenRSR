using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Quaternion fixedRotation;
    Vector3 desiredPosition;

    private void LateUpdate()
    {
        if (target.position.y <= 0.6f)
        {
            desiredPosition = target.position + offset;
        }
        else
        {
            Vector3 newPosition = target.position;
            newPosition.y = 0.6f;
            desiredPosition = newPosition + offset;
        }
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Create a new Quaternion that only contains the fixed rotation you want
        Quaternion newRotation = Quaternion.Euler(fixedRotation.eulerAngles.x, transform.rotation.eulerAngles.y, fixedRotation.eulerAngles.z);

        // Apply the new position and rotation to the camera's transform
        transform.SetPositionAndRotation(smoothedPosition, newRotation);
    }
}