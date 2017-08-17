using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public Text CountText;
    public Text WinText;

    public float speed;
    private int count;
    private int collidersCount;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCountText();
        WinText.text = "";
        collidersCount =GameObject.FindGameObjectsWithTag("Pick Up").Count();
    }

    private void UpdateCountText()
    {
        CountText.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0, moveVertical);            // (x,y,z)

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
        if (count == collidersCount)
            WinText.text = "You Win!";
    }
}
