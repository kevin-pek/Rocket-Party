using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private float timeLeft = 10f;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Text gameoverText;
    [SerializeField] private Button returnToMenu;
    [SerializeField] private PlayerControl player;

    private void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        // Pause if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.isPaused = !player.isPaused;
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(false);
                timerText.gameObject.SetActive(false);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Time.timeScale = 1;
                pauseText.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(true);
                timerText.gameObject.SetActive(true);
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
        }

        timeLeft -= Time.deltaTime;

        // if time has run out, end level
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            Time.timeScale = 0;
            player.isPaused = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            timerText.gameObject.SetActive(false);
            gameoverText.gameObject.SetActive(true);
            returnToMenu.gameObject.SetActive(true);
        }

        DisplayTime();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = "Score: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
