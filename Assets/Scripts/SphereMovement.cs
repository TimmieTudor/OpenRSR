using UnityEngine;
using MethFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
public class SphereMovement : MonoBehaviour
{
    // The speed of the sphere
    public float speed = 8f;
     // The Rigidbody component of the sphere
    private Rigidbody rb;
     // The speed of the sphere after being modified by the MethFunction
    // public float pepsiSpeed;
     // A boolean to check if the sphere is jumping
    public bool isJumping = true;
     // The GameObject that the sphere is jumping on
    private GameObject jumpTile;
    private GameObject normalTile;
    private GameObject glassTile;
    private List<GameObject> glassTiles = new List<GameObject>();
     // The z-position of the sphere when it collides with the jumpTile
    private float collisionZ;
    private void Start()
    {
        // Get the Rigidbody component of the sphere
        rb = GetComponent<Rigidbody>();
         // Create a new instance of the MethFunction
        MethFunction meth = new MethFunction();
         // Modify the speed of the sphere using the MethFunction
        // pepsiSpeed = meth.piEpsilon(speed);
        rb.MovePosition(rb.position + Vector3.back * speed * 0.025f);
        rb.useGravity = false;
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

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    private void FixedUpdate()
    {
        // Create a Vector3 for the movement of the sphere
        Vector3 direction = Vector3.forward;
        Vector3 movement = direction.normalized * speed;
         // Move the sphere according to the movement Vector3
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
         // If the sphere is jumping, call the Jump() method
        if (jumpTile != null && isJumping)
        {
            Jump(3.95f, jumpTile);
        }
        else if (normalTile != null && glassTile != null)
        {
            exponential_falus();
        } else if (glassTiles.Count > 0) {
            FallingGlass();
        } else if (normalTile != null && glassTile == null) {
            exponential_falus();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            // Check if the sphere has collided with a jumpTile
            if (collision.gameObject.tag == "JumpCollision")
            {
                // Set isJumping to true to indicate that the sphere is jumping 
                isJumping = true; 
                 // Disable gravity for the sphere 
                rb.useGravity = false; 
                 // Set the jumpTile to the GameObject the sphere has collided with 
                jumpTile = collision.gameObject; 
                 // Set the collisionZ to the z-position of the sphere 
            }
            else if (collision.gameObject.tag == "NormalTile")
            {
                normalTile = null;
                glassTile = null;
                collisionZ = 0f;
                isJumping = false;
                jumpTile = null;
            } else if (collision.gameObject.tag == "GlassCollision") {
                GameObject glassTileParent = collision.gameObject.transform.parent.gameObject;
                GameObject glassTileNormal = glassTileParent.transform.GetChild(1).gameObject;
                GameObject glassTileActive = glassTileParent.transform.GetChild(2).gameObject;
                glassTileActive.SetActive(true);
                glassTileNormal.SetActive(false);
                normalTile = null;
                glassTile = null;
                collisionZ = 0f;
                isJumping = false;
                jumpTile = null;
            }
        } 
        catch (Exception e) 
        { 
            Debug.LogError("Error in OnCollisionEnter: " + e.Message); 
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "NormalTile")
        {
            normalTile = null;
            glassTile = null;
            collisionZ = 0f;
        } else if (collision.gameObject.tag == "GlassCollision") {
            normalTile = null;
            glassTile = null;
            collisionZ = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "NormalTile")
        {
            normalTile = collision.gameObject;
            glassTile = collision.gameObject;
            rb.useGravity = false;
            collisionZ = collision.gameObject.transform.position.z + 0.1f;
        } else if (collision.gameObject.tag == "GlassCollision") {
            glassTile = collision.gameObject;
            glassTiles.Add(collision.gameObject);
            normalTile = collision.gameObject;
            rb.useGravity = false;
            collisionZ = collision.gameObject.transform.position.z + 0.1f;
        }
    }

    public void FallingGlass() {
        foreach (GameObject m_glassTile in glassTiles) {
            if (m_glassTile == null) continue;
            GameObject glassTileParent = m_glassTile.transform.parent.gameObject;
            GameObject glassTileNormal = glassTileParent.transform.GetChild(1).gameObject;
            GameObject glassTileActive = glassTileParent.transform.GetChild(2).gameObject;
            Vector3 newPosition = new Vector3(glassTileParent.transform.position.x, glassTileParent.transform.position.y - 0.125f, glassTileParent.transform.position.z);
            glassTileParent.transform.position = newPosition;
        }
    }

    private float jumpFunction(float x, float div_coeff) {
        return 2f * speed * ((Mathf.Exp(-x) - Mathf.Exp(x)) / (Mathf.Exp(x) + Mathf.Exp(-x)));
    }

    public void Jump(float distance, GameObject jumpTile) 
    { 
        if (jumpTile == null) return;
        Vector3 newPosition = new Vector3(jumpTile.transform.position.x, jumpTile.transform.position.y + 0.1f, jumpTile.transform.position.z);
        GameObject jumpTileParent = jumpTile.transform.parent.parent.gameObject;
        GameObject jumpTileNormal = jumpTileParent.transform.GetChild(0).gameObject;
        GameObject jumpTileActive = jumpTileParent.transform.GetChild(1).gameObject;
        jumpTile = jumpTileParent;
        /* float p = distance / 3.025f * (-1f); */
        float z = transform.position.z - jumpTile.transform.position.z;
        /*float h = 0f;
        float k = 6.12f;
        float jumpCalc = p * ((float)Math.Pow(z - h, 2)) + k; */
        float jumpCalc = jumpFunction(z - (distance / (2f)), 1f);
        if (z < 0.01f) {
            jumpCalc = 0f;
        }
        isJumping = true;
        if (!jumpTileActive.activeSelf) {
            jumpTileActive.SetActive(true);
            jumpTileNormal.SetActive(false);
        }
        // Create a Vector3 for the upward movement of the sphere 
        Vector3 movement2 = Vector3.up * jumpCalc;
        try 
        { 
            if (z > 0.2f && z < 1.2f && isJumping) {
                newPosition = new Vector3(jumpTile.transform.position.x, jumpTileActive.transform.position.y + 0.1f, jumpTile.transform.position.z);
                jumpTileActive.transform.position = newPosition;
            }
            if (isJumping)
            {
                rb.MovePosition(rb.position + movement2 * Time.fixedDeltaTime);
                //collisionZ += 0.075f;
            }
            // If the zDiff is greater than the distance, set the jumpTile to null and enable gravity for the sphere 
            if (z > distance) 
            { 
                newPosition = new Vector3(jumpTile.transform.position.x, 0f, jumpTile.transform.position.z);
                //jumpTileActive.transform.position = newPosition;
                // jumpTile = null;
                // jumpCalc = 0f;
                // isJumping = false;
                // collisionZ = transform.position.z + 0.03f;
            } 
        } 
        catch (Exception e) 
        { 
            Debug.LogError("Error in Jump: " + e.Message); 
        } 
    }
    // Initialize the boner (exponentially growing dick)
    public void exponential_falus() 
    {
        float z = transform.position.z - collisionZ;
        float downY = jumpFunction(z + 4f, 1f) * -1f;
        Vector3 downVector = Vector3.down * downY;
        rb.MovePosition(rb.position + downVector * Time.fixedDeltaTime);
    }
}