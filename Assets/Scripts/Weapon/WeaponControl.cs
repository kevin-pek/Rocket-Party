using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponControl : MonoBehaviour
{
    public Texture2D cursor;
    protected float cooldown = 1.0f;
    protected float cooldownTimer;
    //include rocket class
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1")) {
            FireWeapon();
        }
        // debug line trace to mouse
        
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        Debug.DrawLine(transform.position, worldPosition);
        
    }

    void FireWeapon() {
        if (cooldownTimer <= 0) {
            cooldownTimer = cooldown;
            // spawn rocket
            Debug.DrawRay(transform.position, GetFireAngle() * Vector3.forward, Color.red);
        }
    }

    Quaternion GetFireAngle() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        return Quaternion.LookRotation(worldPosition - transform.position);
    }
}
