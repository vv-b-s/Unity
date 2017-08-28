using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovemet : MonoBehaviour
{
    public float Speed = 100;
    public float BallRotation = 1;

    private Rigidbody2D ball;
    private float initSpeed;

    void Start ()
    {
        ball = GetComponent<Rigidbody2D>();
        ApplyRandomForce();
        ball.rotation = 1 * Time.deltaTime;
        initSpeed = Speed;
	}

    private void ApplyRandomForce()
    {
        var randomForce = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        ball.AddForce(randomForce * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        Vector2 force = new Vector2();
        if (collisionObject.tag == "Top Wall")
            force = new Vector2(0, -1) * Speed;

        if (collisionObject.tag == "Botom Wall")
            force = new Vector2(0, 1) * Speed;

        if(collisionObject.tag=="StckCorner")
        {
            Speed = (float)Math.Pow(Speed, 2);
            force = new Vector2(1, 0) * Speed;
        }

        if(collisionObject.tag=="StickMiddle")
        {
            if (Speed > initSpeed)
                Speed = (float)Math.Sqrt(Speed);
            if (Speed < initSpeed)
                Speed = initSpeed;

            force = new Vector2(1, 0) * Speed;
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
        ApplyRandomForce();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
