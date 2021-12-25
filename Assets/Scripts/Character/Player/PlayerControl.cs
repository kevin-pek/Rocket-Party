using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : CharacterControl
{
    private int score;
    [SerializeField] private UI ui;
    public bool isPaused;
    [SerializeField] private float setNotInvincibleAfterSeconds = 2.0f;

    public void Start()
    {
        base.Start();
        isPaused = false;
        score = 0;
    }

    public override void TakeDamage()
    {
        spriteBlinkingTotalDuration = setNotInvincibleAfterSeconds;
        base.TakeDamage();

        UpdateScore(false);

        isInvincible = true;
        StartCoroutine(SetNotInvincibleAfter_local());

        IEnumerator SetNotInvincibleAfter_local()
        {
            yield return new WaitForSeconds(setNotInvincibleAfterSeconds);
            isInvincible = false;
        }
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
        ui.UpdateScore(score);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();

        if (isPaused)
        {
            return;
        }

        TickCooldownTimer();

        // Movement
	    var inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.velocity = inputMovement.normalized * characterSpeed;       
        
        // Rotate Sprite
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.SignedAngle(Vector2.up, pos - transform.position);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

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
