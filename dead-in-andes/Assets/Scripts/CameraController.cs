using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dragSpeed = 5f;
    public float boundary = 0.1f;

    public RectTransform canvasRect; // Assign Canvas's RectTransform in the Inspector

    void Start()
    {
        // Find the Canvas in the scene and get its RectTransform component
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("No Canvas found in the scene.");
        }
    }
    
    void Update()
    {
        // Move the canvas horizontally when mouse is close to screen edges
        float mouseX = Input.mousePosition.x;
        float screenWidth = Screen.width;
        float moveDirection = 0f;

        if (mouseX < screenWidth * boundary)
        {
            moveDirection = -1f;
        }
        else if (mouseX > screenWidth * (1 - boundary))
        {
            moveDirection = 1f;
        }

        canvasRect.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);

        // Drag and drop functionality
        if (Input.GetMouseButton(0))
        {
            // Convert mouse position to canvas local position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out Vector2 localPoint);

            // Check if any UI element is being dragged
            foreach (Transform child in canvasRect)
            {
                if (child.gameObject.CompareTag("Draggable"))
                {
                    RectTransform rectTransform = child.GetComponent<RectTransform>();
                    if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
                    {
                        rectTransform.position = Vector2.Lerp(rectTransform.position, localPoint, dragSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }
}


