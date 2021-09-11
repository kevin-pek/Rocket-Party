using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// @todo use Input Actions
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 1.0f;

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
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
        Vector2 velocity = new Vector2();
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed)
            {
                velocity.y += characterSpeed;
            }
            if (keyboard.aKey.isPressed)
            {
                velocity.x -= characterSpeed;
            }
            if (keyboard.sKey.isPressed)
            {
                velocity.y -= characterSpeed;
            }
            if (keyboard.dKey.isPressed)
            {
                velocity.x += characterSpeed;
            }
        }
        print(velocity);
        _rigidbody2D.velocity = velocity;
    }
}
