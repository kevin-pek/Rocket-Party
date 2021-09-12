﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 5.0f;

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
        _rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * characterSpeed;
    }
}
