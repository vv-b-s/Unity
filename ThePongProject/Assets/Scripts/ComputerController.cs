using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputerController : StickMovement
{
    public float ComputerStickForce = 0.35f;
    public List<GameObject> ComputerPoints;

    public static bool IsWinner = false;
    public static int ComputerScores = 0;

    private Vector2 direction;
    private Rigidbody2D Ball { get { return BallMovemet.Ball; } }
    private bool XisIncreasing { get { return BallMovemet.XisIncreasing; } }

    public override void Start()
    {
        direction = new Vector2(0, ComputerStickForce);
        base.Start();
    }
    public override void FixedUpdate()
    {
        if(GameIsNotOver || IsWinner)
        {
            base.FixedUpdate();

            if (XisIncreasing && (Ball.position.y > ComputerPoints[0].transform.transform.position.y || Ball.position.y < ComputerPoints[1].transform.transform.position.y))
            {
                if (Stick.position.y < Ball.position.y)
                    MoveStick(direction);
                else
                    MoveStick(-direction);
            }
        }
        else
            {
                Stick.MoveRotation(1);
                Stick.AddForce(-Vector2.one);
            }
    }
}
