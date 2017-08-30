using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovemet : MonoBehaviour
{
    public float Speed = 50;
    public float BallRotation = 1;
    public float MinimalForce = 1f;
    public float SpeedIncrease = 1.5f;

    [HideInInspector] public static bool XisIncreasing, YisIncrasing;
    [HideInInspector] public static Rigidbody2D Ball;

    private float initMinForce;
    private Vector2 ballPreviousPosition;
    private Vector2 force = Vector2.zero;

    void Start ()
    {
        Ball = GetComponent<Rigidbody2D>();
        ApplyRandomForce();
        initMinForce = MinimalForce;
        ballPreviousPosition = Ball.position;
        Ball.MoveRotation(BallRotation);
    }

    void FixedUpdate()
    {
        if (Ball.position == ballPreviousPosition)
        {
            force.x = XisIncreasing ? MinimalForce : -MinimalForce;
            force.y = YisIncrasing ? MinimalForce : -MinimalForce;

            Ball.AddForce(force);
        }
        DecideDirection();
        ballPreviousPosition = Ball.position;
    }


    private void NewGame()
    {
        Ball.position = Vector2.zero;
        Ball.velocity = Vector2.zero;       // stop the ball
        MinimalForce = initMinForce;

        ApplyRandomForce();
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.tag == "Top Wall"||collisionObject.tag=="Botom Wall")
            RevertYForce();
       
        if (collisionObject.tag == "StickCorner")
        {
            MinimalForce *= SpeedIncrease;

            RevertXForce();
            
        }

        if (collisionObject.tag == "StickMiddle")
        {
            if (MinimalForce > initMinForce)
                MinimalForce /= SpeedIncrease;
            if (MinimalForce< initMinForce)
                MinimalForce = initMinForce;

                RevertXForce();
        }

        if (collisionObject.tag.Contains("Side Wall"))
        {
            Ball.position = Vector2.zero;
            NewGame();
            return;
        }
    }

    private void ApplyRandomForce()
    {
        var moveTowardsPlayer = Random.Range(0, 2) == 1 ? true : false;
        var moveUp = Random.Range(0, 2) == 1 ? true : false;

        force.x = moveTowardsPlayer ? -MinimalForce : MinimalForce;
        force.y = moveUp ? MinimalForce : -MinimalForce;

        var randomForce = force;

        Ball.AddForce(randomForce * Speed);
    }

    private void DecideDirection()
    {
        XisIncreasing = Ball.position.x > ballPreviousPosition.x || force.x > 0;
        YisIncrasing = Ball.position.y > ballPreviousPosition.y || force.y > 0;
    }

    private void RevertXForce()
    {
        force.x = XisIncreasing ? -MinimalForce : MinimalForce;
        force.y = YisIncrasing ? MinimalForce : -MinimalForce;

        Ball.velocity = Vector2.zero;
        Ball.AddForce(force * Speed);
    }

    private void RevertYForce()
    {
        force.x = XisIncreasing ? MinimalForce : -MinimalForce;
        force.y = YisIncrasing ? -MinimalForce : MinimalForce;

        Ball.velocity = Vector2.zero;
        Ball.AddForce(force * Speed);
    }
}
