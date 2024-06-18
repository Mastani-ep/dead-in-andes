using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().Play("TitleAnim");
    }

    public void PlayGame()
    {
        GetComponent<Animator>().Play("FadeInScreenAnim");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void TitleTextAnimationEnd()
    {
        GetComponent<Animator>().Play("StartButtonAnim");
    }
    
    public void StartButtonAnimationEnd()
    {
        GetComponent<Animator>().Play("QuitButtonAnim");
    }

}
