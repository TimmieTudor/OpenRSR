using UnityEngine;

public class NonSphereMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}