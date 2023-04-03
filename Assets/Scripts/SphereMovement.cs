using UnityEngine;
using MethFunctions;
public class SphereMovement : MonoBehaviour
{
    // The speed of the sphere
    public float speed = 8f;
     // The Rigidbody component of the sphere
    private Rigidbody rb;
     // The speed of the sphere after being modified by the MethFunction
    public float pepsiSpeed;
     // A boolean to check if the sphere is jumping
    private bool isJumping = false;
     // The GameObject that the sphere is jumping on
    private GameObject jumpTile;
     // The z-position of the sphere when it collides with the jumpTile
    private float collisionZ;
    private void Start()
    {
        // Get the Rigidbody component of the sphere
        rb = GetComponent<Rigidbody>();
         // Create a new instance of the MethFunction
        MethFunction meth = new MethFunction();
         // Modify the speed of the sphere using the MethFunction
        pepsiSpeed = meth.piEpsilon(speed);
         // Freeze the rotation of the sphere
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void FixedUpdate()
    {
        // Create a Vector3 for the movement of the sphere
        Vector3 movement = transform.forward * pepsiSpeed;
         // Move the sphere according to the movement Vector3
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
         // If the sphere is jumping, call the Jump() method
        if (jumpTile != null)
        {
            Jump(4f, jumpTile);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            // Check if the sphere has collided with a jumpTile
            if (collision.gameObject.tag == "JumpTile")
            {
                // Set isJumping to true to indicate that the sphere is jumping 
                isJumping = true; 
                 // Disable gravity for the sphere 
                rb.useGravity = false; 
                 // Set the jumpTile to the GameObject the sphere has collided with 
                jumpTile = collision.gameObject; 
                 // Set the collisionZ to the z-position of the sphere 
                collisionZ = transform.position.z; 
            } 
        } 
        catch (Exception e) 
        { 
            Debug.LogError("Error in OnCollisionEnter: " + e.Message); 
        } 
    } 
     public void Jump(float distance, GameObject jumpTile) 
    { 
        // Create a Vector3 for the upward movement of the sphere 
        Vector3 movement2 = transform.up * pepsiSpeed; 
         // Create a Vector3 for the downward movement of the sphere 
        Vector3 movement3 = Vector3.down * pepsiSpeed; 
         // Calculate the difference in z-position between the sphere and the jumpTile 
        float zDiff = transform.position.z - jumpTile.transform.position.z; 
         // Calculate the z-difference between the collisionZ and the jumpTile position, and clamp it between -0.05 and 0.31 
        float zDiff2 = Mathf.Clamp(collisionZ - jumpTile.transform.position.z + 0.666f, -0.05f, 0.31f); 
         // Calculate the jumpBoost based on the pepsiSpeed and the zDiff2 
        float jumpBoost = pepsiSpeed + zDiff2 * 1.5f; 
         // Set the movement2 Vector3 to equal the upward movement with the jumpBoost applied 
        movement2 = transform.up * jumpBoost; 
        try 
        { 
            // If the sphere is jumping and the zDiff is less than one-third of the distance, move the sphere upward 
            if (isJumping && zDiff < distance / 3f) 
            { 
                rb.MovePosition(rb.position + movement2 * Time.fixedDeltaTime); 
            } 
            // If the zDiff is greater than one-third and less than one-half of the distance, move the sphere upward at a slower rate 
            if (zDiff > distance / 3f && zDiff < distance / 2f) 
            { 
                rb.MovePosition(rb.position + (movement2 / (distance)) * Time.fixedDeltaTime); 
            } 
            // If the zDiff is greater than one-half and less than two-thirds of the distance, move the sphere downward at the same rate as the upward movement 
            if (zDiff > distance / 2f && zDiff < distance / 1.5f) 
            { 
                rb.MovePosition(rb.position + (movement3 / (distance)) * Time.fixedDeltaTime); 
            } 
            // If the zDiff is greater than two-thirds of the distance, move the sphere downward until it reaches the ground, and set isJumping to false 
            if (zDiff > distance / 1.5f) 
            { 
                rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime); 
                isJumping = false; 
            } 
            // If the sphere is not jumping and the zDiff is greater than two-thirds of the distance and less than the distance, move the sphere downward 
            if (!isJumping && zDiff > distance / 1.5f && zDiff < distance) 
            { 
                rb.MovePosition(rb.position + movement3 * Time.fixedDeltaTime); 
            } 
            // If the zDiff is greater than the distance, set the jumpTile to null and enable gravity for the sphere 
            if (zDiff > distance) 
            { 
                jumpTile = null; 
                rb.useGravity = true; 
            } 
        } 
        catch (Exception e) 
        { 
            Debug.LogError("Error in Jump: " + e.Message); 
        } 
    } 
}