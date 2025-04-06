using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string LoadLevel = "BjornTestSceneClone";
    
    public void PlayGame()
    {
        print(SceneManager.sceneCountInBuildSettings);
        var scene = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
       
}
