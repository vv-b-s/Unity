using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    // Define the points that will be given/taken on each row
    public int WallDamage = 1;
    public int PointsPerFood = 10;
    public int PointsPerSoda = 20;

    // Used whem the player triggers the exit sign so the next level can load
    public float RestartLevelDelay = 1f;

    // used to refer to the animations of the player
    private Animator animator;

    // used to keep the current level food
    private int food;

    /// <summary>
    /// Overriding the start function so the references to player animator and food are instaantiated
    /// </summary>
    protected override void Start()
    {
        animator = GetComponent<Animator>();              // Gets the reference to the animator

        food = GameManager.Instance.PlayerFoodPoints;   // Gets the reference to the foodpoints

        base.Start();                                 // Invokes Start() from MovingObject class
    }

    /// <summary>
    /// When the level has ended the global instancer of FoosPoints will get the current amount of left food
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.PlayerFoodPoints = food;
    }

    /// <summary>
    /// Calls tjhe AttemptMove method which will check whether if the plater can move
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xDir"></param>
    /// <param name="yDir"></param>
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;                                 // food decreases by one every time the player makes a move

        base.AttemptMove<T>(xDir, yDir);       // The base function in MovingObject is called
        RaycastHit2D hit;                     // Declaring RaycastHit2D for the Move() method
        CheckIfGameOver();

        // if move is true a sound will be played
        if (Move(xDir, yDir, out hit));

        //Every time a food is lost the game should be checked if it is over
        CheckIfGameOver();

        // when the move is done the player's turn should be set to false so the enemies can do action
        GameManager.Instance.PlayersTurn = false;
    }

    /// <summary>
    /// Defines actions when the player hits a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", RestartLevelDelay);
            enabled = false;
        }
        else if(other.tag=="Food")
        {
            food += PointsPerFood;
            other.gameObject.SetActive(false);
        }
        else if(other.tag=="Soda")
        {
            food += PointsPerSoda;
            other.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn) return;  // if it is not the player's turn Update won't run

        int horizontal = 0;
        int vertical = 0;

        // take input from the keyboard and convert it to integer value
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        //prevents player from moving diagonaly
        if (horizontal != 0)
            vertical = 0;

        //if input is taken the move attempt is run
        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
    }

    /// <summary>
    /// Checks whether if the game is over
    /// </summary>
    private void CheckIfGameOver()
    {
        // If there is no food the game is over
        if (food <= 0)
            GameManager.Instance.GameOver();
    }


    /// <summary>
    /// Overriding the abstract methood OnCantMove
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="component"></param>
    protected override void OnCantMove<T>(T component)
    {
        //Gets the wall component if the player hits it
        Wall hitWall = component as Wall;
        
        //Damages the wall
        hitWall.DamaageWall(WallDamage);

        //Sets the animation to damaging the wall
        animator.SetTrigger("PlayerChop");
    }

    /// <summary>
    /// Runs when the level is complete
    /// </summary>
    private void Restart()
    {
        //Used to load the next scene
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Invokes when player looses food
    /// </summary>
    /// <param name="loss"></param>
    public void LooseFood(int loss)
    {
        // Set the animation to Player Hit
        animator.SetTrigger("PlayerHit");

        // Decrease the amount of food
        food -= loss;

        // Check if the game is over
        CheckIfGameOver();
    }
}
