using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour
{
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected float rocketCooldownDuration;
    [SerializeField] protected GameObject rocketClass;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected Rigidbody2D rigidBody;
    [HideInInspector]public bool isInvincible = false; // Mutable at runtime
    protected float rocketCooldownTimer = 0.0f;

    [SerializeField] protected GameObject shootEffect;
    protected Vector3 spriteOffset = new Vector3(0.5f, 1.6f, 0); // offset for fire animation
    // for blinking animation
    protected float spriteBlinkingTimer = 0.0f;
    protected float spriteBlinkingMiniDuration = 0.05f;
    protected float spriteBlinkingTotalTimer = 0.0f;
    protected float spriteBlinkingTotalDuration = 1.0f;
    protected bool startBlinking = false;
    [HideInInspector]public Collider2D objectCollider;

    public virtual void TakeDamage() {
        if (isInvincible) return;
        transform.position = spawnPos.position;
        startBlinking = true;
    }

    protected void Update() {
        if (startBlinking == true)
            SpriteBlinkingEffect();
    }
    
    protected virtual void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<Collider2D>();
    }

    protected void TickCooldownTimer()
    {
        if (rocketCooldownTimer > 0.0f)
        {
            rocketCooldownTimer = Mathf.Max(0.0f, rocketCooldownTimer - Time.deltaTime);
        }
    }

    public bool FireWeapon(Vector3 targetPosition)
    {
        if (rocketCooldownTimer > 0.0f)
        {
            return false;
        }

        rocketCooldownTimer = rocketCooldownDuration;
        var angle = Vector2.SignedAngle(Vector2.up, targetPosition - transform.position);
        var rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        var rocket = Instantiate(rocketClass, transform.position, rotation);
        rocket.GetComponent<Rocket>().parentPlayerCollider = objectCollider;
        Physics2D.IgnoreCollision(objectCollider, rocket.GetComponent<Rocket>().objectCollider, true);

        // spawn shoot_effect animation
        GameObject shootEffectObject = Instantiate(shootEffect, transform.position + rotation * spriteOffset, rotation);
        Destroy(shootEffectObject, .2f);
        return true;
    }

    protected void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            GetComponent<SpriteRenderer>().enabled = true;
            return;
        }
     
        spriteBlinkingTimer += Time.deltaTime;
        if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (GetComponent<SpriteRenderer>().enabled == true) {
                GetComponent<SpriteRenderer>().enabled = false;
            } else {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
