using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StickMovement
{
    public static int PlayerScores = 0;
    public static bool IsWinner = false;
    public List<AudioClip> Sounds;

    // Static members used so the sound can be modified from other classes
    private static AudioSource sound;
    private static List<AudioClip> sounds;

    private void Awake()
    {
        sounds = Sounds;
        sound = GetComponent<AudioSource>();
    }

    public override void FixedUpdate()
    {
        // If the game is over the stick should go nuts (Returning the normal physics of the object)
        if (GameIsNotOver || IsWinner)
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
        else
            BreakTheLaws(1);
    }

    public static void PlayScoreSound()
    {
       if(PlayerScores<3)
        {
            sound.clip = sounds.Find(s => s.name == "playerScore");
            sound.Play();
        }
    }
}
