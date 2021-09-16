using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public int maxBounce = 5;
    public string hitTag;

    private int currentBounce = 0;
    private new Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        collider.enabled = false;
        currentBounce = 0;
        StartCoroutine(EnableColliderAfter(0.2f));
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == hitTag)
        {
            collision.collider.GetComponent<CharacterControl>().TakeDamage();
        }

        if (currentBounce >= maxBounce)
        {
            Destroy(transform.gameObject);
            return;
        }

        Vector2 collisionNormal = collision.contacts[0].normal;
        float collisionAngle = Vector2.SignedAngle(-transform.up, collisionNormal);
        transform.up = -transform.up;
        transform.Rotate(Vector3.forward, collisionAngle * 2);
        currentBounce++;
    }

    private IEnumerator EnableColliderAfter(float time)
    {
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }
}
