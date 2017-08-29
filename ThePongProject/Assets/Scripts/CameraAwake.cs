using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAwake : MonoBehaviour
{
    public GameObject Ball;
    private void Awake()
    {
        Instantiate(Ball);
    }
}
