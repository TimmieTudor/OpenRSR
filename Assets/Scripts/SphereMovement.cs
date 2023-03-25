using UnityEngine;
using MethFunctions;

public class SphereMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float pepsiSpeed;

    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MethFunction meth = new MethFunction();
        pepsiSpeed = meth.piEpsilon(speed);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * pepsiSpeed;
        Vector3 movement2 = transform.up * pepsiSpeed;
        Vector3 movement3 = Vector3.down * pepsiSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        if (isJumping && transform.position.y < 2.7f)
        {
            rb.MovePosition(rb.position + movement2 * Time.fixedDeltaTime);
        }
        else if (transform.position.y > 2.7f)
        {
            rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime);
            isJumping = false;
        }
        else if (!isJumping)
        {
            rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpTile")
        {
            isJumping = true;
            rb.useGravity = false;
        }
        else if (collision.gameObject.tag == "NormalTile")
        {
            rb.useGravity = true;
        }
    }
}
