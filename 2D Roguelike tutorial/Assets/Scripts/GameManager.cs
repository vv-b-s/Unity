using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BoardManager BoardScript;                 // Keeps the instance of the randomly generated map
    public static GameManager Instance = null;      // Singleton instance of GameManager. Only one game manager is needed.

    private int level = 5;
    private void Awake()
    {
        // Checking if instance of GameManager allready if not it gives it the value of this instance, if it has instance but is not this, it destroys the object of other instances
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        BoardScript = GetComponent<BoardManager>(); //Get a component reference to the attached BoardManager script
        InitGame();
    }
    /// <summary>
    /// Initialize BoardManager and game components
    /// </summary>
    private void InitGame()
    {
        BoardScript.SetupScene(level);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
