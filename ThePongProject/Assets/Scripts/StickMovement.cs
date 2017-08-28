using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMovement : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D Stick;
    public float Speed = 50;
    private float initRotation;
    private float initX;

    // Use this for initialization
    void Start()
    {
        Stick = GetComponent<Rigidbody2D>();
        initRotation = Stick.rotation;
        initX = Stick.position.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float verticalMovement = Input.GetAxis("Vertical");

        if (Stick.rotation!=initRotation)           // Keeps ridgid body from rotating spontaneously
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

    private void MoveStick(Vector2 movement)
    {
        Stick.velocity = movement*Speed;
    }

    private void OnCollisionEnter(Collision colObj)
    {
        if (colObj.gameObject.tag == "Wall") ;

    }
}
