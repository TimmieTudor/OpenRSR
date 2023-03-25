using UnityEngine;
using MethFunctions;

public class SphereMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float pepsiSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MethFunction meth = new MethFunction();
        pepsiSpeed = meth.piEpsilon(speed);
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * pepsiSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}
