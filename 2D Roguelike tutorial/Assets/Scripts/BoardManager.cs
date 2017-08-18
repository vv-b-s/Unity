using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;  // Prevent conflict with System.Random
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable]      // Manages the storage of the object: http://bit.ly/2vPb4Wr. In the case of unity it allows us to embed a class with sub properties in the inspector.
    public class Count  //Keeps track of the upper and lower amounts a prefab can exist in the world
    {
        public int Minimum;
        public int Maximum;

        public Count(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }
    }

    #region Public Defined Variables
    // Size of the Board 8x8
    public int Columns = 8;
    public int Rows = 8;

    // Allowed amount of inner walls for a level
    public Count WallCount = new Count(5, 9);

    //  Allowed amount of food for a level
    public Count FoodCount = new Count(1, 5);
    #endregion

    #region Public Undefined Variables
    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;
    public GameObject[] OuterWallTiles;
    #endregion

    // keeps the instance of the board grid's transform
    private Transform boardHolder;

    //Keeps all the positions of the grid items
    private List<Vector2> gridPositions = new List<Vector2>();

    /// <summary>
    /// Clears the old list of grid positions and prepares it for a new board.
    /// Takes information about the location of empty floor grid items
    /// </summary>
    void InitialiseList()
    {
        gridPositions.Clear();

        for (var x = 0; x < Columns - 1; x++)
        {
            for (var y = 0; y < Rows - 1; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    /// <summary>
    /// Makes the default view of the game. Sets up the outer walls and floor (background) of the game board.
    /// </summary>
    void BoardSetup()
    {
        // Creates the board and gives its transform to boardHolder
        boardHolder = new GameObject("Board").transform;

        for (var x = -1; x < Columns + 1; x++)  // from one outer side to the next outer side
        {
            for (var y = -1; y < Rows + 1; y++)
            {
                //Returns random floor tile from the floor prefabs
                var toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];

                // Checks if x or y is in the outer borders and if so, an outer wall tile is generated.
                if (x == -1 || x == Columns || y == -1 || y == Rows)
                    toInstantiate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];

                // Instantiates the randomly chosen prefab, giving it a position on the grid.
                var instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                // Makes the newly instantiated object a child to the Board
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    /// <summary>
    /// Gives random coordinates on the free 8x8 floor spaces recorded in the gridPostitions variable and removes it from the list to make it unavailable
    /// </summary>
    /// <returns>Location</returns>
    Vector2 RandomPosition()
    {
        // Defines a random index from the gridPoints
        int randomIndex = Random.Range(0, gridPositions.Count);

        // takes the V2 element inside the list
        var randomPosition = gridPositions[randomIndex];

        //removes the taken element from the list
        gridPositions.RemoveAt(randomIndex);

        // returns the coordinates
        return randomPosition;
    }

    /// <summary>
    /// Spawns prefabs at random positions
    /// </summary>
    /// <param name="tileArray">Set of tiles to render</param>
    /// <param name="minMax">Minimum and maximum amounts</param>
    void LayoutObjectAtRandom(GameObject[] tileArray, Count amounts)
    {
        // Defines the amount of objects that will be generated
        int objectCount = Random.Range(amounts.Minimum, amounts.Maximum + 1);

        for (var i = 0; i < objectCount; i++)
        {
            //Gets a random Vector2 coordinates on the board grid
            var randomPosition = RandomPosition();

            // Rabdomly chooses a prefab variation of the wished tile.
            var tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiates the tile on a random position
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();                                                                        // Makes the default view of the game
        InitialiseList();                                                                   // Clear existing data of grid objects location and create new one

        LayoutObjectAtRandom(WallTiles, WallCount);                                       // Spawns inner walls at random places
        LayoutObjectAtRandom(FoodTiles, FoodCount);                                      // Spawns food ad random places
        int enemyCount = (int)Mathf.Log(level, 2f);                                     // Returns number of enemies calculated by logarithm, so that there is at least one enemy for a level
        LayoutObjectAtRandom(EnemyTiles, new Count(enemyCount, enemyCount));           // Spawns enemis at random places. 
        Instantiate(Exit, new Vector2(Columns - 1, Rows - 1), Quaternion.identity);   // Instantiates Exit prefab to a fixed location
    }
}
