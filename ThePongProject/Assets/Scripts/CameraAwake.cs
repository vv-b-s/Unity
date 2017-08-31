using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAwake : MonoBehaviour
{
    public static bool GameIsNotOver = true;

    public GameObject Ball;
    public float VisibleWidth;
    private void Awake()
    {
        Instantiate(Ball);
    }

    private void LateUpdate()
    {    // Keep the screen resolution alwys showing the game scene
        // http://answers.unity3d.com/questions/760671/resizing-orthographic-camera-to-fit-2d-sprite-on-s.html
        Camera.main.orthographicSize = VisibleWidth * Screen.height / Screen.width * 0.5f;
    }
}
