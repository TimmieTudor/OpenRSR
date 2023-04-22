using UnityEngine;
using MethFunctions;
using System;
using System.Collections;
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
        rb.MovePosition(rb.position + Vector3.back * pepsiSpeed * 0.025f);
         // Freeze the rotation of the sphere
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    IEnumerator waitBeforeLoading()
    {
        Debug.Log("Waiting...");
        yield return new WaitForSeconds(1f);
        Debug.Log("Done!");
        enabled = true;
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
        float p = distance / 3f * (-1f);
        float z = transform.position.z - jumpTile.transform.position.z;
        float h = 0f;
        float k = distance + (distance / 2f);
        float jumpCalc = p * ((float)Math.Pow(z - h, 2)) + k;
        // Create a Vector3 for the upward movement of the sphere 
        Vector3 movement2 = transform.up * jumpCalc;
        try 
        { 
            if (z < distance && isJumping)
            {
                rb.MovePosition(rb.position + movement2 * Time.fixedDeltaTime);
            }
            // If the zDiff is greater than the distance, set the jumpTile to null and enable gravity for the sphere 
            if (z > distance) 
            { 
                jumpTile = null; 
                rb.useGravity = true;
                jumpCalc = 0f;
            } 
        } 
        catch (Exception e) 
        { 
            Debug.LogError("Error in Jump: " + e.Message); 
        } 
    } 
}