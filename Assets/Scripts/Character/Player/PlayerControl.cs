using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
    [SerializeField] private float characterSpeed = 1.0f;
    private Rigidbody2D _rigidbody2D;
    public Transform spawnPos;
    public Text hitText;

    public override void TakeDamage()
    {
        print("Ahhhhh!!!");
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
	    var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody2D.velocity = input.normalized * characterSpeed;
    }
}
