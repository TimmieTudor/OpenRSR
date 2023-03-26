using UnityEngine;
using MethFunctions;

public class SphereMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float pepsiSpeed;

    private bool isJumping = false;
    private GameObject jumpTile;

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
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        if (jumpTile != null)
        {
            Jump(4.25f, jumpTile);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpTile")
        {
            isJumping = true;
            rb.useGravity = false;
            jumpTile = collision.gameObject;
        }
        else if (collision.gameObject.tag == "NormalTile")
        {
            rb.useGravity = true;
        }
    }

    public void Jump(float distance, GameObject jumpTile)
    {
        Vector3 movement2 = transform.up * pepsiSpeed;
        Vector3 movement3 = Vector3.down * pepsiSpeed;
        float zDiff = transform.position.z - jumpTile.transform.position.z;
        if (isJumping && zDiff < distance / 3f)
        {
            rb.MovePosition(rb.position + movement2 * Time.fixedDeltaTime);
        }
        if (zDiff > distance / 3f && zDiff < distance / 2f)
        {
            rb.MovePosition(rb.position + (movement2 / (distance)) * Time.fixedDeltaTime);
        }
        if (zDiff > distance / 2f && zDiff < distance / 1.5f)
        {
            rb.MovePosition(rb.position + (movement3 / (distance)) * Time.fixedDeltaTime);
        }
        if (zDiff > distance / 1.5f)
        {
            rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime);
            isJumping = false;
        }
        if (!isJumping)
        {
            rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime);
        }
        if (zDiff > distance)
        {
            jumpTile = null;
        }
    }
}
