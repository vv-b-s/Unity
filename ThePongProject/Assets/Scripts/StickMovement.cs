using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StickMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Stick;
    [HideInInspector] public float initX;
    public float Speed = 50;

    public bool GameIsNotOver { get { return CameraAwake.GameIsNotOver; } }

    // Use this for initialization
    public virtual void Start()
    {
        Stick = GetComponent<Rigidbody2D>();
        initX = Stick.position.x;
    }

    public virtual void FixedUpdate()
    {
        // Keeps ridgid body from rotating spontaneously
        if (Stick.rotation != 0)
            Stick.rotation = 0;

        // Prevent stick from moving away if it colides with the ball
        if (Stick.position.x != initX)
            Stick.position = new Vector2(initX, Stick.position.y);
    }

    public void MoveStick(Vector2 movement)
    {
        Stick.velocity = movement * Speed;
    }

    /// <summary>
    /// Destroys the written laws in a fun way
    /// </summary>
    /// <param name="positiveOrNegative"></param>
    public virtual void BreakTheLaws(int positiveOrNegative)
    {
        Stick.rotation = UnityEngine.Random.Range(0, 1f);
        Stick.AddForce(positiveOrNegative >= 0 ? Vector2.one : -Vector2.one);
    }
}
