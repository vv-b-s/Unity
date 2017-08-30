using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputerController : StickMovement
{
    public float ComputerStickForce = 0.35f;
    public List<GameObject> ComputerPoints;

    private Vector2 direction;

    public override void Start()
    {
        direction = new Vector2(0, ComputerStickForce);
        base.Start();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (BallMovemet.XisIncreasing 
            && 
            (BallMovemet.Ball.position.y > ComputerPoints[0].transform.transform.position.y
            || 
            BallMovemet.Ball.position.y < ComputerPoints[1].transform.transform.position.y))
        {
            if (Stick.position.y < BallMovemet.Ball.position.y)
                MoveStick(direction);
            else
                MoveStick(-direction);
        }
    }
}
