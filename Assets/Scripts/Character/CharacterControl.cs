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
    protected float rocketCooldownTimer = 0.0f;

    public abstract void TakeDamage();

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
        rocket.GetComponent<Rocket>().parentPlayer = gameObject;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), rocket.GetComponent<Collider2D>(), true);
        return true;
    }
}
