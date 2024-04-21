using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Singleton instance
    public static BackgroundController instance;

    // Bounds of the background
    public float leftBound = -10f; // Change these values according to your background size
    public float rightBound = 10f;

    void Awake()
    {
        // Ensure only one instance of BackgroundController exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
