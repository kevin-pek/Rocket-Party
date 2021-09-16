using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour
{
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected float rocketCooldownDuration;
    [SerializeField] protected GameObject rocketClass;
    protected float rocketCooldownTimer = 0.0f;

    public abstract float TGetFireAngle();

    public abstract void TakeDamage();  

    public bool FireWeapon()
    {
        if (rocketCooldownTimer > 0.0f)
        {
            return false;
        }
        rocketCooldownTimer = rocketCooldownDuration;
        Instantiate(rocketClass, transform.position, Quaternion.AngleAxis(TGetFireAngle(), Vector3.up));
        return true;
    }
}
