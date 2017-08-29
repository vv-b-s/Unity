using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StickMovement : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D Stick;
    [HideInInspector]public float initRotation, initX;
    public float Speed = 50;

    // Use this for initialization
    public virtual void Start()
    {
        Stick = GetComponent<Rigidbody2D>();
        initRotation = Stick.rotation;
        initX = Stick.position.x;
    }

    public abstract void FixedUpdate();

    public void MoveStick(Vector2 movement)
    {
        Stick.velocity = movement*Speed;
    }
}
