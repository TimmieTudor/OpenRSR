using UnityEngine;
using MethFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> fallingObstacles = new List<GameObject>();
    private List<GameObject> glassGroup1 = new List<GameObject>();
    private List<GameObject> fallingObstaclesGroup1 = new List<GameObject>();
    private bool hitGroup1 = false;
    private List<GameObject> glassGroup2 = new List<GameObject>();
    private List<GameObject> fallingObstaclesGroup2 = new List<GameObject>();
    private bool hitGroup2 = false;
    private List<GameObject> glassGroup3 = new List<GameObject>();
    private List<GameObject> fallingObstaclesGroup3 = new List<GameObject>();
    private bool hitGroup3 = false;
    private GameManager manager;
     // The z-position of the sphere when it collides with the jumpTile
    private float collisionZ;
    private void Start()
    {
        // Get the Rigidbody component of the sphere
        rb = GetComponent<Rigidbody>();
        manager = GetComponent<GameManager>();
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
        //Debug.Log(manager.isGamePaused);
        //Debug.Log(manager.isGameOver);
        if (!manager.isGamePaused) {
            // Create a Vector3 for the movement of the sphere
            Vector3 direction = Vector3.forward;
            Vector3 movement = direction.normalized * speed;
            // Move the sphere according to the movement Vector3
            rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
            // If the sphere is jumping, call the Jump() method
            if (jumpTile != null && isJumping)
            {
                Jump(3.95f, jumpTile);
            }
            else if (normalTile != null && glassTile != null && !manager.isGamePaused)
            {
                exponential_falus();
            } else if (glassTiles.Count > 0) {
                FallingGlass();
            } /*else if (normalTile != null && glassTile == null) {
                exponential_falus();
            } else if (normalTile == null && glassTile != null) {
                exponential_falus();
            } */
        }
    }

    private void ActivateGlassTilesG1(GameObject currentGlassTile) {
        if (hitGroup1) return;
        GameObject currentGlassTileParent = currentGlassTile.transform.parent.gameObject;
        GameObject currentGlassTileNormal = currentGlassTileParent.transform.GetChild(1).gameObject;
        GameObject currentGlassTileActive = currentGlassTileParent.transform.GetChild(2).gameObject;
        currentGlassTileActive.SetActive(true);
        currentGlassTileNormal.SetActive(false);
        glassGroup1.Add(currentGlassTile);
        normalTile = null;
        glassTile = null;
        collisionZ = 0f;
        isJumping = false;
        jumpTile = null;
        List<GameObject> c_risers = GameObject.FindGameObjectsWithTag("Riser").ToList();
        if (c_risers.Any(c_riser => c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x)) {
        foreach (GameObject c_riser in c_risers) {
            if (c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x) {
                fallingObstaclesGroup1.Add(c_riser);
                break;
            }
        }
        }
        List<GameObject> c_glassTiles = GameObject.FindGameObjectsWithTag("GlassCollisionGroup1").ToList();
        foreach (GameObject c_glassTile in c_glassTiles) {
            if (!glassGroup1.Contains(c_glassTile)) {
                if (Mathf.Abs(c_glassTile.transform.position.z - currentGlassTileParent.transform.position.z) <= 1f) {
                    ActivateGlassTilesG1(c_glassTile);
                } else if (Mathf.Abs(c_glassTile.transform.position.x - currentGlassTileParent.transform.position.x) <= 1f) {
                    ActivateGlassTilesG1(c_glassTile);
                }
            }
        }
    }

    private void ActivateGlassTilesG2(GameObject currentGlassTile) {
        if (hitGroup2) return;
        GameObject currentGlassTileParent = currentGlassTile.transform.parent.gameObject;
        GameObject currentGlassTileNormal = currentGlassTileParent.transform.GetChild(1).gameObject;
        GameObject currentGlassTileActive = currentGlassTileParent.transform.GetChild(2).gameObject;
        currentGlassTileActive.SetActive(true);
        currentGlassTileNormal.SetActive(false);
        glassGroup2.Add(currentGlassTile);
        normalTile = null;
        glassTile = null;
        collisionZ = 0f;
        isJumping = false;
        jumpTile = null;
        List<GameObject> c_risers = GameObject.FindGameObjectsWithTag("Riser").ToList();
        if (c_risers.Any(c_riser => c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x)) {
        foreach (GameObject c_riser in c_risers) {
            if (c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x) {
                fallingObstaclesGroup2.Add(c_riser);
                break;
            }
        }
        }
        List<GameObject> c_glassTiles = GameObject.FindGameObjectsWithTag("GlassCollisionGroup2").ToList();
        foreach (GameObject c_glassTile in c_glassTiles) {
            if (!glassGroup2.Contains(c_glassTile)) {
                if (Mathf.Abs(c_glassTile.transform.position.z - currentGlassTileParent.transform.position.z) <= 1f) {
                    ActivateGlassTilesG2(c_glassTile);
                } else if (Mathf.Abs(c_glassTile.transform.position.x - currentGlassTileParent.transform.position.x) <= 1f) {
                    ActivateGlassTilesG2(c_glassTile);
                }
            }
        }
    }

    private void ActivateGlassTilesG3(GameObject currentGlassTile) {
        if (hitGroup3) return;
        GameObject currentGlassTileParent = currentGlassTile.transform.parent.gameObject;
        GameObject currentGlassTileNormal = currentGlassTileParent.transform.GetChild(1).gameObject;
        GameObject currentGlassTileActive = currentGlassTileParent.transform.GetChild(2).gameObject;
        currentGlassTileActive.SetActive(true);
        currentGlassTileNormal.SetActive(false);
        glassGroup3.Add(currentGlassTile);
        normalTile = null;
        glassTile = null;
        collisionZ = 0f;
        isJumping = false;
        jumpTile = null;
        List<GameObject> c_risers = GameObject.FindGameObjectsWithTag("Riser").ToList();
        if (c_risers.Any(c_riser => c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x)) {
        foreach (GameObject c_riser in c_risers) {
            if (c_riser.transform.position.z == currentGlassTileParent.transform.position.z && c_riser.transform.position.x == currentGlassTileParent.transform.position.x) {
                fallingObstaclesGroup3.Add(c_riser);
                break;
            }
        }
        }
        List<GameObject> c_glassTiles = GameObject.FindGameObjectsWithTag("GlassCollisionGroup3").ToList();
        foreach (GameObject c_glassTile in c_glassTiles) {
            if (!glassGroup3.Contains(c_glassTile)) {
                if (Mathf.Abs(c_glassTile.transform.position.z - currentGlassTileParent.transform.position.z) <= 1f) {
                    ActivateGlassTilesG3(c_glassTile);
                } else if (Mathf.Abs(c_glassTile.transform.position.x - currentGlassTileParent.transform.position.x) <= 1f) {
                    ActivateGlassTilesG3(c_glassTile);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            //Debug.Log(hitGroup1);
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
                GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
                foreach (GameObject riser in risers) {
                    if (riser.transform.position.z == glassTileParent.transform.position.z && riser.transform.position.x == glassTileParent.transform.position.x) {
                        fallingObstacles.Add(riser);
                    }
                }
            } else if (collision.gameObject.tag == "GlassCollisionGroup1" && !hitGroup1) {
                ActivateGlassTilesG1(collision.gameObject);
                hitGroup1 = true;
            } else if (collision.gameObject.tag == "GlassCollisionGroup2" && !hitGroup2) {
                ActivateGlassTilesG2(collision.gameObject);
                hitGroup2 = true;
            } else if (collision.gameObject.tag == "GlassCollisionGroup3" && !hitGroup3) {
                ActivateGlassTilesG3(collision.gameObject);
                hitGroup3 = true;
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
        } else if (collision.gameObject.tag == "GlassCollisionGroup1") {
            normalTile = null;
            glassTile = null;
            collisionZ = 0f;
        } else if (collision.gameObject.tag == "GlassCollisionGroup2") {
            normalTile = null;
            glassTile = null;
            collisionZ = 0f;
        } else if (collision.gameObject.tag == "GlassCollisionGroup3") {
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
        } else if (collision.gameObject.tag == "GlassCollisionGroup1") {
            hitGroup1 = true;
            glassTile = collision.gameObject;
            foreach (GameObject m_GlassTile in glassGroup1) {
                if (m_GlassTile == null) continue;
                glassTiles.Add(m_GlassTile);
            }
            foreach (GameObject m_Riser in fallingObstaclesGroup1) {
                if (m_Riser == null) continue;
                fallingObstacles.Add(m_Riser);
            }
            normalTile = collision.gameObject;
            rb.useGravity = false;
            collisionZ = collision.gameObject.transform.position.z + 0.1f;
            hitGroup1 = false;
        } else if (collision.gameObject.tag == "GlassCollisionGroup2") {
            hitGroup2 = true;
            glassTile = collision.gameObject;
            foreach (GameObject m_GlassTile in glassGroup2) {
                if (m_GlassTile == null) continue;
                glassTiles.Add(m_GlassTile);
            }
            foreach (GameObject m_Riser in fallingObstaclesGroup2) {
                if (m_Riser == null) continue;
                fallingObstacles.Add(m_Riser);
            }
            normalTile = collision.gameObject;
            rb.useGravity = false;
            collisionZ = collision.gameObject.transform.position.z + 0.1f;
            hitGroup2 = false;
        } else if (collision.gameObject.tag == "GlassCollisionGroup3") {
            hitGroup3 = true;
            glassTile = collision.gameObject;
            foreach (GameObject m_GlassTile in glassGroup3) {
                if (m_GlassTile == null) continue;
                glassTiles.Add(m_GlassTile);
            }
            foreach (GameObject m_Riser in fallingObstaclesGroup3) {
                if (m_Riser == null) continue;
                fallingObstacles.Add(m_Riser);
            }
            normalTile = collision.gameObject;
            rb.useGravity = false;
            collisionZ = collision.gameObject.transform.position.z + 0.1f;
            hitGroup3 = false;
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
        foreach(GameObject riser in fallingObstacles) {
            if (riser == null) continue;
            if (riser.TryGetComponent<RiserAnim>(out RiserAnim riserAnim)) {
                foreach (Frame frame in riserAnim.animator.frames) {
                    frame.position = new Vector3(frame.position.x, frame.position.y - 0.125f, frame.position.z);
                }
                foreach (Frame frame in riserAnim.animator2.frames) {
                    frame.position = new Vector3(frame.position.x, frame.position.y - 0.125f, frame.position.z);
                }
            } else if (riser.TryGetComponent<CrusherAnim>(out CrusherAnim crusherAnim)) {
                foreach (Frame frame in crusherAnim.animator.frames) {
                    frame.position = new Vector3(frame.position.x, frame.position.y - 0.125f, frame.position.z);
                }
                foreach (Frame frame in crusherAnim.animator2.frames) {
                    frame.position = new Vector3(frame.position.x, frame.position.y - 0.125f, frame.position.z);
                }
            }
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
        if (!manager.isGameOver) {  
            float z = transform.position.z - collisionZ;
            float downY = jumpFunction(z + 4f, 1f) * -1f;
            Vector3 downVector = Vector3.down * downY;
            rb.MovePosition(rb.position + downVector * Time.fixedDeltaTime);
        }
    }

    public void ClearFallingObstacles() {
        glassTiles.Clear();
        fallingObstacles.Clear();
        glassGroup1.Clear();
        fallingObstaclesGroup1.Clear();
        glassGroup2.Clear();
        fallingObstaclesGroup2.Clear();
        glassGroup3.Clear();
        fallingObstaclesGroup3.Clear();
    }
}