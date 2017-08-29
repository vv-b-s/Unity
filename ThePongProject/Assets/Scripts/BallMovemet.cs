using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO: Fix Ball strange behavior
public class BallMovemet : MonoBehaviour
{
    public float Speed = 50;
    public float BallRotation = 1;
    public float MinimalForce = 1.3f;

    private Rigidbody2D ball;
    private float initMinForce;
    private bool XisIncreasing;
    private bool YisIncrasing;
    private Vector2 ballPreviousPosition;

    void Start ()
    {
        ball = GetComponent<Rigidbody2D>();
        ApplyRandomForce();
        ball.rotation = 1 * Time.deltaTime;
        initMinForce = MinimalForce;
        ballPreviousPosition = ball.position;
	}

    void Update()
    {
        if (ball.position == ballPreviousPosition)
        {
            var yForce = YisIncrasing ? MinimalForce : -MinimalForce;
            var xForce = XisIncreasing ? MinimalForce : -MinimalForce;
            ball.AddForce(new Vector2(xForce, yForce));
        }
        DecideDirection();
        ballPreviousPosition = ball.position;
    }


    private void NewGame()
    {
        ball.position = Vector2.zero;
        ball.velocity = Vector2.zero;
        ApplyRandomForce();
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.tag == "Top Wall"||collisionObject.tag=="Botom Wall")
            RevertYForce();
       
        if (collisionObject.tag == "StickCorner")
        {
            MinimalForce = (float)Math.Pow(MinimalForce, 2);

            if (collisionObject.name.Contains("Player"))
                RevertXForce();
            
        }

        if (collisionObject.tag == "StickMiddle")
        {
            if (MinimalForce > initMinForce)
                MinimalForce = (float)Math.Sqrt(MinimalForce);
            if (MinimalForce< initMinForce)
                MinimalForce = initMinForce;

            if (collisionObject.name.Contains("Player"))
                RevertXForce();
        }

        if (collisionObject.tag.Contains("Side Wall"))
        {
            ball.position = Vector2.zero;
            NewGame();
            return;
        }
    }

    private void ApplyRandomForce()
    {
        var moveTowardsPlayer = Random.Range(0, 2) == 1 ? true : false;
        var moveUp = Random.Range(0, 2) == 1 ? true : false;

        var xForce = moveTowardsPlayer ? -MinimalForce : MinimalForce;
        var yForce = moveUp ? MinimalForce : -MinimalForce;

        var randomForce = new Vector2(xForce, yForce);

        ball.AddForce(randomForce * Speed);
    }

    private void DecideDirection()
    {
        XisIncreasing = ball.position.x > ballPreviousPosition.x;
        YisIncrasing = ball.position.y > ballPreviousPosition.y;
    }

    private void RevertXForce()
    {
        var xForce = XisIncreasing ? -MinimalForce : MinimalForce;
        var yForce = YisIncrasing ? MinimalForce : -MinimalForce;

        ball.velocity = Vector2.zero;
        ball.AddForce(new Vector2(xForce, yForce) * Speed);
    }

    private void RevertYForce()
    {
        var xForce = XisIncreasing ? MinimalForce : -MinimalForce;
        var yForce = YisIncrasing ? -MinimalForce : MinimalForce;

        ball.velocity = Vector2.zero;
        ball.AddForce(new Vector2(xForce, yForce) * Speed);
    }
}
