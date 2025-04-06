using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LoadLevel = "NameHere";
    
    public void PlayGame()
    {
        SceneManager.LoadScene(LoadLevel);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
       
}
