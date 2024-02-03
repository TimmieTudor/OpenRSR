using UnityEngine;

public class SphereDragger : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private float screenWidth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            screenWidth = Screen.width;
            // Get the current mouse position and convert it to world coordinates
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = this.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Convert the mouse position to a value between -1 and 1
            float normalizedX = (mousePos.x / screenWidth) * 2f - 1f;

            // Update the x position of the sphere based on the mouse position
            float xPos = normalizedX * 3.1f;
            rb.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z));
        }
    }
}