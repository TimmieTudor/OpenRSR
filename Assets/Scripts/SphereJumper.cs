using UnityEngine;

public class SphereJumper : MonoBehaviour
{
    public float jumpForce = 0.2f;
    private bool isMovingX = false;
    private GameObject jumpTile;
    private bool isJumping = false;
    private SphereMovement sphm;

    private enum JumpState
    {
        DOWN = 0,
        MIDAIR = 1,
        UP = 2,
        FALLING = 3
    }

    private JumpState jumpState = JumpState.DOWN;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphm = GetComponent<SphereMovement>();

        // Freeze rotation in X, Y, and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (jumpTile != null)
        {
            if (isJumping)
            {
                jumpState = JumpState.UP;
                rb.useGravity = false;
            }
            else 
            {
                jumpState = JumpState.DOWN;
            }

            if (jumpState != JumpState.FALLING)
            {
                float targetYPos = (float)jumpState + 0.55f;
                float currentYPos = transform.position.y;
                float yDiff = targetYPos - currentYPos;

                float newJumpForce = jumpForce * sphm.speed;
                
                float yForce = yDiff * newJumpForce;
                if (transform.position.y < 0.61f || (transform.position.y >= 2.65f && !isJumping))
                {
                    rb.AddForce(Vector3.up * yForce, ForceMode.Force);
                }
                else if (transform.position.y >= 2.8f && isJumping)
                {
                    rb.AddForce(Vector3.up * yForce, ForceMode.Force);
                }
            }
            else 
            {
                rb.useGravity = true;
            }
            
            float zDiff = transform.position.z - jumpTile.transform.position.z;
            float maxZDiff = 2.9f - (sphm.pepsiSpeed / sphm.speed);
            if (zDiff >= maxZDiff)
            {
                jumpState = JumpState.DOWN;
                isJumping = false;
            }
            if (zDiff >= 4f)
            {
                jumpState = JumpState.FALLING;
                isJumping = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpTile"))
        {
            jumpTile = collision.gameObject;
            isJumping = true;
        }
        else if (collision.gameObject.CompareTag("NormalTile"))
        {
            jumpState = JumpState.FALLING;
            isJumping = false;
            rb.useGravity = true;
        }
    }

    public void SetMovingX(bool isMoving)
    {
        isMovingX = isMoving;
    }
}
