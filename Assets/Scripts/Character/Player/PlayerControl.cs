using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{    
    [SerializeField] private Text hitText;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private float hideHitTextAfterSeconds = 1.0f;
    [SerializeField] private float setNotInvincibleAfterSeconds = 2.0f;

    public override void TakeDamage()
    {
        spriteBlinkingTotalDuration = setNotInvincibleAfterSeconds;
        base.TakeDamage();

        hitText.gameObject.SetActive(true);
        StartCoroutine(HideHitTextAfter_local());

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

    // Update is called once per frame
    private void Update()
    {
        base.Update();

        TickCooldownTimer();

        // Movement
	    var inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.velocity = inputMovement.normalized * characterSpeed;       
        
        // Rotate Sprite
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.SignedAngle(Vector2.up, pos - transform.position);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

        // Fire Weapon
        if (Input.GetButton("Fire1"))
        {
            FireWeaponAtMouseWorldPosition();
        }

        // Debug Trace
        if (Debug.isDebugBuild)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            Debug.DrawLine(transform.position, worldPosition);
        }
    }

    private void FireWeaponAtMouseWorldPosition()
    {
        FireWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
