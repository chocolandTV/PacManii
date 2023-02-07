using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGrid : MonoBehaviour
{
    // GRID 21 x 21  DD: Leere Felder ( bewegung möglich)
    /*
      BLUE WALLS , Grey Entry, Pellets, Great Pellets4 & Teleport
      CONDITIONS:
      BLUEWALLS CANT ENTERED#
      public variable Pacman position 

    */
    // CREATE GRID  
    /*
    MazeConcept
    Node class exist of PACMANS, GHOSTS*4 ( mode)
    GRID  21 *21
    */
    public Transform pacmanPos { get; private set; }
    public bool[,] mazeGrid { get; private set; }
    [SerializeField] public GameObject level;
    private int MazeSize = 21;
    private Vector3 offset;
    // public void GetPacManPos()
    // public void nextturn() LEFT, RIGHT, TOP, DOWN
    // PACMAN POSITON 10, 0 
    private void Start()
    {
      offset = new Vector3 (-(MazeSize/2),(MazeSize/2),0);
        mazeGrid = new bool[MazeSize, MazeSize];
        for (int x = 0; x < MazeSize; x++)
        {
            for (int y = MazeSize-1; y >= 0; y--)
            {
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(x,y, -4)+transform.position+ offset,Vector3.forward);
                //Camera.main.ScreenPointToRay(new Vector3(x, y, -1));
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Wall")
                    {
                        mazeGrid[x, y] = false;
                    }
                    else
                    {
                        mazeGrid[x, y] = true;
                    }
                }
            }
            // WEGE IMPLEMENTIEREN ALLES ANDERE IS FALSE
        }

    }
}
