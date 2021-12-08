using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : CharacterControl
{    
    public int score;
    [SerializeField] private float timeLeft = 10f;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Text hitText;
    [SerializeField] private Text gameoverText;
    [SerializeField] private Button returnToMenu;
    public bool isPaused;
    [SerializeField] private float hideHitTextAfterSeconds = 1.0f;
    [SerializeField] private float setNotInvincibleAfterSeconds = 2.0f;

    public void Start()
    {
        base.Start();
        isPaused = false;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        score = 0;
        var rectTransform = canvas.GetComponent<RectTransform>();
        float screenWidth = rectTransform.rect.width;
        float screenHeight = rectTransform.rect.height;
        timerText.rectTransform.anchoredPosition = new Vector2(0, screenHeight / 2 - 20);
        scoreText.rectTransform.anchoredPosition = new Vector2(0, screenHeight / 2 - 50);
    }

    public override void TakeDamage()
    {
        spriteBlinkingTotalDuration = setNotInvincibleAfterSeconds;
        base.TakeDamage();

        UpdateScore(false);

        //hitText.gameObject.SetActive(true);
        //StartCoroutine(HideHitTextAfter_local());

        isInvincible = true;
        StartCoroutine(SetNotInvincibleAfter_local());

        IEnumerator HideHitTextAfter_local()
        {
            yield return new WaitForSeconds(hideHitTextAfterSeconds);
            hitText.gameObject.SetActive(false);
        }

        IEnumerator SetNotInvincibleAfter_local()
        {
            yield return new WaitForSeconds(setNotInvincibleAfterSeconds);
            isInvincible = false;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60); 
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = "Score: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(bool isIncrement)
    {
        if (isIncrement) {
            score += 100;
        }
        else
        {
            score = Mathf.Max(0, score - 50);
        }
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    private void Update()
    {
        // Pause if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
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

        base.Update();
        // reduce level timer
        timeLeft -= Time.deltaTime;

        // if time has run out, end level
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            Time.timeScale = 0;
            isPaused = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            timerText.gameObject.SetActive(false);
            gameoverText.gameObject.SetActive(true);
            returnToMenu.gameObject.SetActive(true);
        }

        DisplayTime(timeLeft);
        TickCooldownTimer();

        // Movement
	    var inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.velocity = inputMovement.normalized * characterSpeed;       
        
        // Rotate Sprite
        if (isPaused == false) {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var angle = Vector2.SignedAngle(Vector2.up, pos - transform.position);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        }

        // Fire Weapon
        if (Input.GetButton("Fire1") && isPaused == false)
        {
            FireWeaponAtMouseWorldPosition();
        }

        // Debug Trace
        /*
        if (Debug.isDebugBuild)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            Debug.DrawLine(transform.position, worldPosition);
        }*/
    }

    private void FireWeaponAtMouseWorldPosition()
    {
        FireWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
