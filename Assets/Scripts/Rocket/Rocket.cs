﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public int maxBounce = 5;
    public string hitTag;

    [HideInInspector] public GameObject parentPlayer;

    private int currentBounce = 0;
    [HideInInspector] public Collider2D objectCollider;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject rocketAudioObject;
    private RocketAudio rocketAudio;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        currentBounce = 0;
        StartCoroutine(EnableColliderAfter(0.2f));
        GameObject obj = Instantiate(rocketAudioObject, Vector3.zero, Quaternion.identity);
        rocketAudio = obj.GetComponent<RocketAudio>();
        rocketAudio.PlayFireSound();
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == hitTag) // Character
        {
            if (collision.collider.GetComponent<CharacterControl>().isInvincible)
                return;
            if (!collision.collider.GetComponent<PlayerControl>() && parentPlayer.GetComponent<PlayerControl>())
                parentPlayer.GetComponent<PlayerControl>().UpdateScore(true);
            Vector2 hitPoint = collision.GetContact(0).point;
            GameObject effect = Instantiate(explosionEffect, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
            rocketAudio.PlayExplosionSound();
            Destroy(effect, 1);
            collision.collider.GetComponent<CharacterControl>().TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.collider.tag == objectCollider.tag) { // Rocket
            Vector2 hitPoint = collision.GetContact(0).point;
            GameObject effect = Instantiate(explosionEffect, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
            rocketAudio.PlayExplosionSound();
            Destroy(effect, 1);
            Destroy(gameObject);
        }
        else if (currentBounce >= maxBounce)
        {
            Vector2 hitPoint = collision.GetContact(0).point;
            GameObject effect = Instantiate(explosionEffect, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
            rocketAudio.PlayExplosionSound();
            Destroy(effect, 1);
            Destroy(gameObject);
            return;
        }
        else { //bounce
            Vector2 hitPoint = collision.GetContact(0).point;
            GameObject effect = Instantiate(hitEffect, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
            Destroy(effect, 0.5f);
            Vector2 collisionNormal = collision.contacts[0].normal;
            float collisionAngle = Vector2.SignedAngle(-transform.up, collisionNormal);
            transform.up = -transform.up;
            transform.Rotate(Vector3.forward, collisionAngle * 2);
            currentBounce++;
        }
    }

    private IEnumerator EnableColliderAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreCollision(parentPlayer.GetComponent<Collider2D>(), objectCollider, false);
    }
}
