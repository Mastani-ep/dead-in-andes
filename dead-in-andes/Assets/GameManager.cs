using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static GameManager instance;
    [SerializeField] private GameObject PauseGameMenu;

    public static GameManager Instance
    {
        get => instance;
        set => instance = value;
    }

    [SerializeField] private int lockingInteractionCount = 0;

    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public int LockedParallaxScrolling
    {
        get => lockingInteractionCount;
        set => lockingInteractionCount = value;
    }

    public void IncreaseLockingInteractionCount()
    {
        lockingInteractionCount++;
    }

    public void DecreaseLockingInteractionCount()
    {
        lockingInteractionCount--;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseGameMenu.activeSelf)
            {
                IncreaseLockingInteractionCount();
                PauseGameMenu.SetActive(true);
            }else
            {
                DecreaseLockingInteractionCount();
                PauseGameMenu.SetActive(false);
            }
        }
    }
}
