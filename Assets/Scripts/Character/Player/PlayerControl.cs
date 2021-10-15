using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{    
    [SerializeField] private Text hitText;
    [SerializeField] private Texture2D cursor;

    public override void TakeDamage()
    {
        transform.position = spawnPos.position;
        hitText.gameObject.SetActive(true);
        StartCoroutine(HideHitTextAfter(1));
    }

    private IEnumerator HideHitTextAfter(float time)
    {
        yield return new WaitForSeconds(time);
        hitText.gameObject.SetActive(false);
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
