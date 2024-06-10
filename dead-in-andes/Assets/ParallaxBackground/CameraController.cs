using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 0.4f;
    public float boundary = 0.1f;
    public float buttonTranslation = 25f;

    void Start()
    {
    }
    
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float screenWidth = Screen.width;
        int parallaxLock = GameManager.Instance.LockedParallaxScrolling;
        if (Input.GetKeyDown(KeyCode.D) && parallaxLock == 0)
        {
            translateByKey(1f, buttonTranslation);
            return;
        }

        // Move left when A key is pressed
        if (Input.GetKeyDown(KeyCode.A) && parallaxLock == 0)
        {
            translateByKey(-1f, buttonTranslation);
            return;
        }

        float distanceFromCenter = Mathf.Abs(mouseX - screenWidth / 2);
        
        if (distanceFromCenter < screenWidth * 0.45 || parallaxLock != 0)
        {
            return;
        }
        
        float direction = mouseX < screenWidth / 2 ? -1f : 1f;
        Vector3 movement = new Vector3(direction * moveSpeed * Time.deltaTime, 0f, 0f);
        
        transform.Translate(movement);
        
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -boundary, boundary);
        transform.position = clampedPosition;
    }

    public void translateByKey(float direction, float distance)
    {
        Vector3 movement = new Vector3(direction * distance, 0f, 0f);
        transform.Translate(movement);
        
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -boundary, boundary);
        transform.position = clampedPosition;
    }
}


