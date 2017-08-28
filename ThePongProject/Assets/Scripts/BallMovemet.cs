using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO: Fix Ball strange behavior
public class BallMovemet : MonoBehaviour
{
    public float Speed = 100;
    public float BallRotation = 1;
    public float MinimalForce = 2;

    private Rigidbody2D ball;
    private float initSpeed;
    private bool XisIncreasing;
    private bool YisIncrasing;
    private Vector2 ballPreviousPosition;

    void Start ()
    {
        ball = GetComponent<Rigidbody2D>();
        ApplyRandomForce();
        ball.rotation = 1 * Time.deltaTime;
        initSpeed = Speed;
        ballPreviousPosition = ball.position;
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

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        Vector2 force = new Vector2();
        float xForce;
        float yForce;

        if (collisionObject.tag == "Top Wall")
        {
            xForce = XisIncreasing ? MinimalForce : -MinimalForce;
            force = new Vector2(xForce, -MinimalForce) * Speed;
        }

        if (collisionObject.tag == "Botom Wall")
        {
            xForce = XisIncreasing ? MinimalForce : -MinimalForce; 
            force = new Vector2(xForce, MinimalForce) * Speed;
        }

        if(collisionObject.tag=="StckCorner")
        {
            Speed = (float)Math.Pow(Speed, MinimalForce);
            
            if(collisionObject.name.Contains("Player"))
            {
                yForce = YisIncrasing ? MinimalForce : -MinimalForce;
                force = new Vector2(MinimalForce, yForce) * Speed;
            }
        }

        if(collisionObject.tag=="StickMiddle")
        {
            if (Speed > initSpeed)
                Speed = (float)Math.Sqrt(Speed);
            if (Speed < initSpeed)
                Speed = initSpeed;

            if (collisionObject.name.Contains("Player"))
            {
                yForce = YisIncrasing ? MinimalForce : -MinimalForce;
                force = new Vector2(MinimalForce, yForce) * Speed;
            }
        }

        if(collisionObject.tag.Contains("Side Wall"))
        {
            ball.position = Vector2.zero;
            NewGame();
            return;
        }


        ball.AddForce(force);
    }

    private void NewGame()
    {
        ball.position = Vector2.zero;
        ball.velocity = Vector2.zero;
        ApplyRandomForce();
    }

    // Update is called once per frame
    void Update ()
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
}
