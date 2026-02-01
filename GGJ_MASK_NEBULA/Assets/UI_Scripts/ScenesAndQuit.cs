using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ScenesAndQuit: MonoBehaviour
{

    public int sceneNum;


    public void LoadSceneNum()
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }

}