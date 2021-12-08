using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevelMenu() {
        SceneManager.LoadScene("LevelMenu");
    }

    public void LoadLevel(string levelName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
