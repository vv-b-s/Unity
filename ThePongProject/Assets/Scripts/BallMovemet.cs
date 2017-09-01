using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovemet : MonoBehaviour
{
    #region Public Variables
    public float Speed = 50;
    public float BallRotation = 1;
    public float MinimalForce = 1f;
    public float SpeedIncrease = 1.5f;
    public List<AudioClip> Sounds; 
    #endregion

    private AudioSource sound;
    

    // Make shorter access to the variable
    public static bool GameIsNotOver
    {
        get { return CameraAwake.GameIsNotOver; }
        set { CameraAwake.GameIsNotOver = value; }
    }

    // Define the direction of the ball so physics can work properly
    public static bool XisIncreasing, YisIncrasing;

    public static Rigidbody2D Ball;

    // keep the current and previous variables in order to make decisions foe thw ball direction and speed
    private float initMinForce;
    private Vector2 ballPreviousPosition;

    // Unified variable for defining the force
    private Vector2 force = Vector2.zero;

    void Start ()
    {
        Ball = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();

        initMinForce = MinimalForce;
        ballPreviousPosition = Ball.position;

        NewGame();
    }

    void FixedUpdate()
    {
        // If ball is stuck it should decide where to move
        if (Ball.position == ballPreviousPosition)
        {
            force.x = XisIncreasing ? MinimalForce : -MinimalForce;
            force.y = YisIncrasing ? MinimalForce : -MinimalForce;

            Ball.AddForce(force);
        }

        DecideDirection();
        ballPreviousPosition = Ball.position;
    }


    /// <summary>
    /// Resets the ball's position back to zero and decides if game is over
    /// </summary>
    private void NewGame()
    {
        if (PlayerController.PlayerScores == 3 || ComputerController.ComputerScores == 3)
            GameOver();

        Ball.position = Vector2.zero;
        Ball.velocity = Vector2.zero;       // stop the ball
        MinimalForce = initMinForce;
        Ball.MoveRotation(BallRotation);

        sound.clip = Sounds.Find(s => s.name == "bump");

        ApplyRandomForce();
    }

    /// <summary>
    /// If one of the players has hit the maximum scores the game is set to be over and the ball is destroyed
    /// </summary>
    private void GameOver()
    {
        GameIsNotOver = false;
        if (PlayerController.PlayerScores == 3)
            PlayerController.IsWinner = true;
        else ComputerController.IsWinner = true;
        Destroy(GameObject.Find("Ball(Clone)"));
    }

    /// <summary>
    /// If there is nothing to make the ball move, a random force is applied so the game can continue on
    /// </summary>
    private void ApplyRandomForce()
    {
        // Randomly decide whether if the ball should go to the player or the computer
        var moveTowardsPlayer = Random.Range(0, 2) == 1 ? true : false;

        // Decide if the ball should go upwards or downwards
        var moveUp = Random.Range(0, 2) == 1 ? true : false;

        force.x = moveTowardsPlayer ? -MinimalForce : MinimalForce;
        force.y = moveUp ? MinimalForce : -MinimalForce;

        Ball.AddForce(force * Speed);
    }

    /// <summary>
    /// Check where the ball is heading to so it can bounce correctly and make the computer decide where to go.
    /// </summary>
    private void DecideDirection()
    {
        XisIncreasing = Ball.position.x > ballPreviousPosition.x || force.x > 0;
        YisIncrasing = Ball.position.y > ballPreviousPosition.y || force.y > 0;
    }

    /// <summary>
    /// Changes the X direction of the ball without touching the Y
    /// </summary>
    private void RevertXForce()
    {
        force.x = XisIncreasing ? -MinimalForce : MinimalForce;
        force.y = YisIncrasing ? MinimalForce : -MinimalForce;

        Ball.velocity = Vector2.zero;
        Ball.AddForce(force * Speed);
    }

    /// <summary>
    /// Changes the Y direction of the ball without touching the X
    /// </summary>
    private void RevertYForce()
    {
        force.x = XisIncreasing ? MinimalForce : -MinimalForce;
        force.y = YisIncrasing ? -MinimalForce : MinimalForce;

        Ball.velocity = Vector2.zero;
        Ball.AddForce(force * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.tag == "Top Wall" || collisionObject.tag == "Botom Wall")
        {
            RevertYForce();
            sound.Play();
        }

        if (collisionObject.tag == "StickCorner")
        {
            // Speed up the ball due to collision with small surface
            MinimalForce *= SpeedIncrease;
            sound.Play();

            RevertXForce();
        }

        if (collisionObject.tag == "StickMiddle")
        {
            // Slow down the ball due to collision with big surface but keeping the initial speed a a constant
            if (MinimalForce > initMinForce)
                MinimalForce /= SpeedIncrease;
            if (MinimalForce < initMinForce)
                MinimalForce = initMinForce;

            sound.Play();

            RevertXForce();
        }

        // Acts like natural hit on a wall
        if (collisionObject.tag.Contains("Side Wall"))
        {
            Ball.position = Vector2.zero;

            if (collisionObject.name.Contains("Right"))
            {
                PlayerController.PlayerScores++;
                PlayerController.PlayScoreSound();
            }
            else
            {
                ComputerController.ComputerScores++;
                ComputerController.PlayScoreSound();
            }

            NewGame();
            return;
        }
    }
}
