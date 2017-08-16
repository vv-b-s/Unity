using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Game Objects
    public GameObject Player;
    public GameObject WestWall;
    #endregion

    #region Offsets
    private Vector3 offsetBetweenPlayerAndCamera;
    private Vector3 curentOffsetBetweenPlayerAndWestWall;
    private Vector3 initOffsetBetweenPlayerAndWestWall;
    #endregion

    #region Constants
    const float CameraUpMovemetTriggerLimit = 0;
    #endregion

    #region Variables
    float cameraMovementRate = 0;
    int movementDirection;      // -1 down | 0 none | 1 up
    #endregion

    // Use this for initialization
    void Start () {
        offsetBetweenPlayerAndCamera = transform.position - Player.transform.position;
         initOffsetBetweenPlayerAndWestWall = curentOffsetBetweenPlayerAndWestWall = Player.transform.position - WestWall.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            movementDirection = -1;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            movementDirection = 1;
        else movementDirection = 0;

        AlignCamera();
        RotateCamera();
	}

    private void RotateCamera()
    {
        transform.LookAt(Player.transform);         // https://forum.unity3d.com/threads/rotate-the-camera-around-the-object.47353/
    }

    private void AlignCamera()
    {
        transform.position = Player.transform.position + offsetBetweenPlayerAndCamera;
        if (Player.transform.position.z<CameraUpMovemetTriggerLimit)
        {
            curentOffsetBetweenPlayerAndWestWall = Player.transform.position - WestWall.transform.position;
            cameraMovementRate = (initOffsetBetweenPlayerAndWestWall.z - curentOffsetBetweenPlayerAndWestWall.z) / 2;
            transform.position += new Vector3(0, cameraMovementRate / 2, cameraMovementRate * 3);
        }
    }
}
