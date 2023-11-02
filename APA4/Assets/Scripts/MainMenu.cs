using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameEasy()
    {
        SceneManager.LoadScene("_Scene_0");
    }
    public void PlayGameMedium()
    {
        SceneManager.LoadScene("Medium_Scene");
    }
    public void PlayGameHard()
    {
        SceneManager.LoadScene("Hard_Scene");
    }
}
