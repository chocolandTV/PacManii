using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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


    public bool[,] mazeGrid { get; private set; }
    private GameObject level;
    public int MazeSize = 21;
    private Vector3 offset;

    private void Start()
    {
        level = this.gameObject;
        offset = new Vector3(-(MazeSize / 2), -(MazeSize / 2), 0);
        mazeGrid = new bool[MazeSize, MazeSize];
        // ARRAYS VON UNTEN NACH OBEN DURCHMAPEN //

        for (int x = 0; x < MazeSize; x++)
        {
            for (int y = MazeSize - 1; y >= 0; y--)
            {
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(x, y, -4) + transform.position + offset, Vector3.forward);
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
                else
                {

                }
            }
        }
        PrintArray();

    }
    // public void drawGizimos(int seconds, Vector3 position)
    // {
    //     position += new Vector3((int)MazeSize/2+1,(int)MazeSize/2+1,0);
    //     // DIR UP
    //     if((int)position.y+1 < MazeSize)
    //     {
    //         if(!mazeGrid[(int)position.x,(int)position.y+1]){
    //             GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //             cube.transform.position = new Vector3((int)position.x-(int)MazeSize/2, (int)position.y+1-(int)MazeSize/2, -2);
    //             Destroy(cube,seconds);
    //         }
    //     }
    //     // DIR DOWN 
    //     if((int)position.y-1 < MazeSize)
    //     {
    //         if(!mazeGrid[(int)position.x,(int)position.y-1]){
    //             GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //             cube.transform.position = new Vector3((int)position.x-(int)MazeSize/2, (int)position.y-1-(int)MazeSize/2, -2);
    //             Destroy(cube,seconds);
    //         }
    //     }
    //     // DIR LEFT
    //     if((int)position.x-1 < MazeSize)
    //     {
    //         if(!mazeGrid[(int)position.x-1,(int)position.y]){
    //             GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //             cube.transform.position = new Vector3((int)position.x-1-(int)MazeSize/2, (int)position.y-(int)MazeSize/2, -2);
    //             Destroy(cube,seconds);
    //         }
    //     }
    //     // DIR RIGHT
    //     if((int)position.x+1 < MazeSize)
    //     {
    //         if(!mazeGrid[(int)position.x+1,(int)position.y]){
    //             GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //             cube.transform.position = new Vector3((int)position.x+1-(int)MazeSize/2, (int)position.y-(int)MazeSize/2, -2);
    //             Destroy(cube,seconds);
    //         }
    //     }
    // }
    public bool CheckIfDirValid(Vector2 dir, Vector3 position)
    {
        Vector3 roundPos = Vector3Int.RoundToInt(position);
        Vector3 Gridposition = roundPos +
         new Vector3(Mathf.RoundToInt(MazeSize / 2), Mathf.RoundToInt(MazeSize / 2), 0);
        // Debug.Log("POSITIONOFFSET: " + position + " DirectionX: " + dir.x + " DirectionY: " + dir.y);

        // CHECK IF DIRECTION IS UP
        if (dir.y == 1.0f && Gridposition.y + 1 < MazeSize)
        {
            if (mazeGrid[(int)Gridposition.x, (int)Gridposition.y + 1] )
            {
                // Debug.Log("PACMAN MOVE UP");
                return true;
            }else
            if(position.y < roundPos.y) // IF BORDER IS NEXT
            {
                return true;
            }

        }
        // CHECK IF DIRECTION IS DOWN
        if (dir.y == -1.0f && Gridposition.y - 1 >= 0)
        {
            if(mazeGrid[(int)Gridposition.x, (int)Gridposition.y - 1])
            {
                //  Debug.Log("PACMAN MOVE DOWN");
                return true;
            }else
            if(position.y > roundPos.y)// IF BORDER IS NEXT
            {
                return true;
            }
        }
        // CHECK IF DIRECTION IS LEFT
        if (dir.x == -1.0f && Gridposition.x - 1 >= 0)
        {
            if (mazeGrid[(int)Gridposition.x - 1, (int)Gridposition.y])
            {
                // Debug.Log("PACMAN MOVE LEFT");
                return true;
            }else
            if(position.x > roundPos.x)// IF BORDER IS NEXT
            {
                return true;
            }
        }
        // CHECK IF DIRECTION IS RIGHT
        if (dir.x == 1.0f && Gridposition.x + 1 < MazeSize)
        {
            if (mazeGrid[(int)Gridposition.x + 1, (int)Gridposition.y])
            {
                // Debug.Log("PACMAN MOVE RIGHT");
                return true;
            }else
            if(position.x < roundPos.x)// IF BORDER IS NEXT
            {
                return true;
            }
        }

        return false;
    }
    public void PrintArray()
    {

        string result = "";
        for (int y = MazeSize - 1; y >= 0; y--)
        {
            for (int x = 0; x < mazeGrid.GetLength(0); x++)
            {
                if (mazeGrid[x, y])
                {
                    result += "O";
                    // CREATE PRIMITIVE
                    //  GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //  cube.transform.position = new Vector3(x-(int)MazeSize/2, y-(int)MazeSize/2, -4);
                }
                else
                {
                    result += "#";
                }
            }
            result += "\n";
        }
        Debug.Log("Current Mazegrid: \n" + result);
    }

}
