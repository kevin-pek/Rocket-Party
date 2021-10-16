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
    [SerializeField] protected bool isInvincible = false; // Mutable at runtime
    protected float rocketCooldownTimer = 0.0f;

    [SerializeField] protected GameObject shootEffect;
    public Vector3 spriteOffset = new Vector3(0.8f, 2f, 0);
    // for blinking animation
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.05f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;
    public Collider2D objectCollider;

    public virtual void TakeDamage() {
        startBlinking = true;
        if (isInvincible) return;
        transform.position = spawnPos.position;
    }

    protected void Start() {
        objectCollider = GetComponent<Collider2D>();
    }

    protected void Update() {
        if (startBlinking == true)
            SpriteBlinkingEffect();
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

        var rocket = Instantiate(rocketClass, transform.position + rotation * new Vector3(0.5f, 0, 0), rotation);
        rocket.GetComponent<Rocket>().parentPlayer = this;
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
