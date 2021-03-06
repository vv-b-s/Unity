﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region Game objects
    public GameObject GameOverSign;
    public GameObject Divider;
    public GameObject[] PlayerScoresPrefab;
    public GameObject[] ComputerScoresPrefab;
    #endregion

    #region Sound Controll
    public List<AudioClip> Sounds;
    private AudioSource sound;
    private bool gameOverSoundIsPlayed = false; 
    #endregion

    private int PlayerScores { get { return PlayerController.PlayerScores; } }
    private int ComputerScores { get { return ComputerController.ComputerScores; } }

	void Start ()
    {
        PlayerScoresPrefab[0].SetActive(true);
        ComputerScoresPrefab[0].SetActive(true);

        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        DeactivateAllScores();
        if (PlayerScores >= 0 && PlayerScores <= 2 && ComputerScores >= 0 && ComputerScores <= 2)
        {
            PlayerScoresPrefab[PlayerScores].SetActive(true);
            ComputerScoresPrefab[ComputerScores].SetActive(true);
        }
        else
        {
            GameOverSign.SetActive(true);
            Divider.SetActive(false);

            if(!gameOverSoundIsPlayed)
            {
                sound.clip = ComputerController.IsWinner ?
                    Sounds.Find(s => s.name == "gameOver") :
                    Sounds.Find(s => s.name == "playerWin");
                sound.Play();
                gameOverSoundIsPlayed = true;
            }
        }

    }

    private void DeactivateAllScores()
    {
        foreach (var score in PlayerScoresPrefab)
            score.SetActive(false);

        foreach (var score in ComputerScoresPrefab)
            score.SetActive(false);
    }
}
