using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StickMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Stick;
    [HideInInspector]public float initRotation, initX;
    public float Speed = 50;

    public static bool GameIsNotOver
    {
        get { return CameraAwake.GameIsNotOver; }
        set { CameraAwake.GameIsNotOver = value; }
    }

    // Use this for initialization
    public virtual void Start()
    {
        Stick = GetComponent<Rigidbody2D>();
        initRotation = Stick.rotation;
        initX = Stick.position.x;
    }

    public virtual void FixedUpdate()
    {
        if (Stick.rotation != initRotation)           // Keeps ridgid body from rotating spontaneously
            Stick.rotation = initRotation;

        if (Stick.position.x != initX)             // Prevent stick from moving away if it colides with the ball
            Stick.position = new Vector2(initX, Stick.position.y);
    }

    public void MoveStick(Vector2 movement)
    {
        Stick.velocity = movement*Speed;
    }
}
