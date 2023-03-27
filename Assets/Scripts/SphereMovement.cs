using UnityEngine;
using MethFunctions;

public class SphereMovement : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody rb;
    public float pepsiSpeed;

    private bool isJumping = false;
    private GameObject jumpTile;
    private float collisionZ;

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
            Jump(4f, jumpTile);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpTile")
        {
            isJumping = true;
            rb.useGravity = false;
            jumpTile = collision.gameObject;
            collisionZ = transform.position.z;
        }
    }

    public void Jump(float distance, GameObject jumpTile)
    {
        Vector3 movement2 = transform.up * pepsiSpeed;
        Vector3 movement3 = Vector3.down * pepsiSpeed;
        float zDiff = transform.position.z - jumpTile.transform.position.z;
        float zDiff2 = Mathf.Clamp(collisionZ - jumpTile.transform.position.z + 0.666f, -0.05f, 0.31f);
        float jumpBoost = pepsiSpeed + zDiff2 * 1.5f;
        Debug.Log(zDiff2);
        movement2 = transform.up * jumpBoost;
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
        if (!isJumping && zDiff > distance / 1.5f && zDiff < distance)
        {
            rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime);
        }
        if (zDiff > distance)
        {
            jumpTile = null;
            rb.useGravity = true;
        }
    }
}
