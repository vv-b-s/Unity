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
    float cameraMovementRate;
    Vector3 cameraMovement;
    #endregion

    // Use this for initialization
    void Start ()
    {
        offsetBetweenPlayerAndCamera = transform.position - Player.transform.position;
        initOffsetBetweenPlayerAndWestWall = curentOffsetBetweenPlayerAndWestWall = Player.transform.position - WestWall.transform.position;
        cameraMovementRate = 0;
        cameraMovement = new Vector3(0, 0, 0);

    }
	
	// Update is called once per frame
	void LateUpdate () 
    {
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
            cameraMovement.y = cameraMovementRate / 2;
            cameraMovement.z = cameraMovementRate * 3;
            transform.position += cameraMovement;
        }
    }
}
