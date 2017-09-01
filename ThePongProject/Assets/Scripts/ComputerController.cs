using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputerController : StickMovement
{
    public float ComputerStickForce = 0.35f;
    public List<AudioClip> Sounds;

    // Static members used so the sound can be modified from other classes
    private static AudioSource sound;
    private static List<AudioClip> sounds;

    private void Awake()
    {
        sounds = Sounds;
        sound = GetComponent<AudioSource>();
    }

    // Keeps the upper and lower point of the computer's Stick so it can decide if it is in the range of the ball
    // without smoorh movements
    public List<GameObject> ComputerPoints;

    public static bool IsWinner = false;
    public static int ComputerScores = 0;

    // Keeps the information about where the computer's stick should head to when the ball is aproaching
    private Vector2 direction;

    //Easy Refference to the ball
    private Rigidbody2D Ball { get { return BallMovemet.Ball; } }

    // Easy refference to the ball's X axis
    private bool XisIncreasing { get { return BallMovemet.XisIncreasing; } }

    public override void Start()
    {
        direction = new Vector2(0, ComputerStickForce);
        base.Start();
    }
    public override void FixedUpdate()
    {
        // If the game is over the stick should go nuts (Returning the normal physics of the object)
        if (GameIsNotOver || IsWinner)
        {
            base.FixedUpdate();

            // If the ball is aproaching and it is not between the computer points
            if (XisIncreasing && (Ball.position.y > ComputerPoints[0].transform.transform.position.y || Ball.position.y < ComputerPoints[1].transform.transform.position.y))
            {
                if (Stick.position.y < Ball.position.y)
                    MoveStick(direction);
                else
                    MoveStick(-direction);
            }
        }
        else
            BreakTheLaws(-1);
    }

    public static void PlayScoreSound()
    {
        if(ComputerScores<3)
        {
            sound.clip = sounds.Find(s => s.name == "computerScore");
            sound.Play();
        }
    }
}
