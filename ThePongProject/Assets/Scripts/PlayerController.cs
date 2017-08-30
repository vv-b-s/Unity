using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StickMovement
{
    public override void FixedUpdate()
    {
        float verticalMovement = Input.GetAxis("Vertical");

        base.FixedUpdate();

        if (verticalMovement > 0 || verticalMovement < 0)
        {
            var movement = new Vector2(0, Speed / 100 * verticalMovement);
            MoveStick(movement);
        }
        else
            Stick.velocity = Vector2.zero;
    }
}
