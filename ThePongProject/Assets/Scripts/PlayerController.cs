using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StickMovement
{
    public override void FixedUpdate()
    {
        float verticalMovement = Input.GetAxis("Vertical");

        if (Stick.rotation != initRotation)           // Keeps ridgid body from rotating spontaneously
            Stick.rotation = initRotation;

        if (Stick.position.x != initX)             // Prevent stick from moving away if it colides with the ball
            Stick.position = new Vector2(initX, Stick.position.y);

        if (verticalMovement > 0 || verticalMovement < 0)
        {
            var movement = new Vector2(0, Speed / 100 * verticalMovement);
            MoveStick(movement);
        }
        else
            Stick.velocity = Vector2.zero;
    }
}
