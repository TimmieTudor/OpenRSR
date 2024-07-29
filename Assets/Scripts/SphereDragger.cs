using UnityEngine;

public class SphereDragger : MonoBehaviour
{
    public bool isDragging = false;
    public float maxOffset = 6.5f;
    private Rigidbody rb;
    private GameManager manager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !manager.isGamePaused && !manager.isGameOver) {
            isDragging = true;
        } else if (Input.GetMouseButtonUp(0) && !manager.isGamePaused && !manager.isGameOver) {
            isDragging = false;
        }
        if (isDragging && !manager.isGamePaused && !manager.isGameOver)
        {
            //Debug.Log("Dragging");
            /*if (Screen.width != screenWidth) {
                screenWidth = Screen.width;
            } */
            // Get the current mouse position and convert it to world coordinates
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = this.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Convert the mouse position to a value between -1 and 1
            float normalizedX = ((mousePos.x) / Screen.width) * 2f - 1f;

            // Update the x position of the sphere based on the mouse position
            float xPos = maxOffset * normalizedX * Screen.width / Screen.height;
            rb.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z));
        }
    }
}