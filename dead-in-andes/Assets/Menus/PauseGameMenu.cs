using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenu : MonoBehaviour
{
    public GameObject exitScreen;
    public void ReturnToMainMenu()
    {
        exitScreen.gameObject.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ResumeGame()
    {
        GameManager.Instance.DecreaseLockingInteractionCount();
        gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}
