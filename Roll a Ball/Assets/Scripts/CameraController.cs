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
    const float MaxCameraXRotationAngle = 0.5f;
    const float CameraUpMovemetTriggerLimit = 0;
    #endregion

    #region Variables
    float cameraMovementRate = 0;
    Vector3 cameraRotationRate = new Vector3(0, 0, 0);
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
        if (Player.transform.position.z < CameraUpMovemetTriggerLimit &&
            this.transform.rotation.x < MaxCameraXRotationAngle &&
            movementDirection==-1)
        {
            cameraRotationRate.x = cameraMovementRate;
            transform.Rotate(cameraRotationRate);
        }

        else if(Player.transform.position.z < CameraUpMovemetTriggerLimit &&
            this.transform.rotation.x <= MaxCameraXRotationAngle &&
            movementDirection==1)
        {
            cameraRotationRate.x = -cameraMovementRate;
            transform.Rotate(cameraRotationRate);
        }
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
