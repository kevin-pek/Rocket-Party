using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevels() {
        SceneManager.LoadScene("Levels");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
