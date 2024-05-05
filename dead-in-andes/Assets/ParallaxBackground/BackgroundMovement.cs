using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float moveModifier = 0.3f;
    [SerializeField] private float maxPosition;
    [SerializeField] private float minPosition;

    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChangePosition(); 
    }

    public void ChangePosition()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RectTransform rectTransform = GetComponent<RectTransform>();
        mousePosition.x -= Screen.width / 2;
        float rect_transform_pos_x = rectTransform.anchoredPosition.x;
        
        if (Mathf.Abs(mousePosition.x) > 350)  // 350 is a temporary measure, should be a percentage of screen width halfed
        {
            float posX = Mathf.Lerp(0, -mousePosition.x * moveModifier, 0.5f * Time.deltaTime);
            posX = MathF.Min(Mathf.Max(posX, minPosition - rect_transform_pos_x), maxPosition - rect_transform_pos_x);
            rectTransform.anchoredPosition = new Vector3(rect_transform_pos_x + posX, rectTransform.anchoredPosition.y, 0);
        }
    }
}
