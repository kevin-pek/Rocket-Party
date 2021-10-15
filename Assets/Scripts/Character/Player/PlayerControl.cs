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

    // TODO animate invincibility
    public override void TakeDamage()
    {
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
        TickCooldownTimer();

        // Movement
	    var inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.velocity = inputMovement.normalized * characterSpeed;       
        
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
