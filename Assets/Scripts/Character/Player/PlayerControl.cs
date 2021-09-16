using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{    
    private Rigidbody2D _rigidbody2D;
    public Transform spawnPos;
    public Text hitText;
    public Texture2D cursor;

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

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError("CharacterMovement missing RigidBody2D");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
	    var inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody2D.velocity = inputMovement.normalized * characterSpeed;
        
        if (rocketCooldownTimer > 0.0f)
        {
            rocketCooldownTimer = Mathf.Max(0.0f, rocketCooldownTimer - Time.deltaTime);
        }
        
        if (Input.GetButton("Fire1"))
        {
            FireWeapon();
        }

        if (Debug.isDebugBuild)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            Debug.DrawLine(transform.position, worldPosition);
        }
    }

    public override float TGetFireAngle() {
        Vector2 diff = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var x = Vector2.SignedAngle(Vector2.right, diff);
        Debug.Log(x);
        return x;
    }
}
