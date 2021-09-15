using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;
        float collisionAngle = Vector2.SignedAngle(-transform.up, collisionNormal);
        //print(collisionAngle);
        transform.up = -transform.up;
        transform.Rotate(Vector3.forward, collisionAngle * 2);
    }
}
