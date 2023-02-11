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
    [SerializeField] public GameObject level;
    public int MazeSize = 21;
    private Vector3 offset;

    private void Start()
    {
        
        offset = new Vector3 (-(MazeSize/2),-(MazeSize/2),0);
        mazeGrid = new bool[MazeSize, MazeSize];
        for (int x = 0; x < MazeSize; x++)
        {
            for (int y = MazeSize-1; y >= 0; y--)
            {
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(x,y, -4)+transform.position+ offset,Vector3.forward);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Wall")
                    {
                        mazeGrid[x,y] = false;
                        
                    }
                    else
                    {
                        mazeGrid[x, y] = true;
                        
                    }
                }else{
                    
                }
            } 
        }
        PrintArray();
        
    }
    public void drawGizimos(int seconds, Vector3 position)
    {
        position += new Vector3((int)MazeSize/2+1,(int)MazeSize/2+1,0);
        // DIR UP
        if((int)position.y+1 < MazeSize)
        {
            if(!mazeGrid[(int)position.x,(int)position.y+1]){
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3((int)position.x-(int)MazeSize/2, (int)position.y+1-(int)MazeSize/2, -2);
                Destroy(cube,seconds);
            }
        }
        // DIR DOWN 
        if((int)position.y-1 < MazeSize)
        {
            if(!mazeGrid[(int)position.x,(int)position.y-1]){
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3((int)position.x-(int)MazeSize/2, (int)position.y-1-(int)MazeSize/2, -2);
                Destroy(cube,seconds);
            }
        }
        // DIR LEFT
        if((int)position.x-1 < MazeSize)
        {
            if(!mazeGrid[(int)position.x-1,(int)position.y]){
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3((int)position.x-1-(int)MazeSize/2, (int)position.y-(int)MazeSize/2, -2);
                Destroy(cube,seconds);
            }
        }
        // DIR RIGHT
        if((int)position.x+1 < MazeSize)
        {
            if(!mazeGrid[(int)position.x+1,(int)position.y]){
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3((int)position.x+1-(int)MazeSize/2, (int)position.y-(int)MazeSize/2, -2);
                Destroy(cube,seconds);
            }
        }
    }
    public Vector2 CheckNextMove(Vector2 dir, Vector3 position)
    {   
        
        position += new Vector3((int)MazeSize/2,(int)MazeSize/2,0);
        // Debug.Log("POSITIONOFFSET: " + position + " DirectionX: " + dir.x + " DirectionY: " + dir.y);
        Vector2 result = Vector2.zero;
        // CHECK IF DIRECTION IS UP
        if(dir.y == 1.0f && (int)position.y+1 < MazeSize)
        {
            if(mazeGrid[(int)position.x,(int)position.y+1]){
                // Debug.Log("PACMAN MOVE UP");
                return Vector2.up;}
                
        }
        // CHECK IF DIRECTION IS DOWN
        if(dir.y == -1.0f && (int)position.y-1 >= 0)
        {
           if(mazeGrid[(int)position.x, (int)position.y-1]){
            //  Debug.Log("PACMAN MOVE DOWN");
               return  Vector2.down;}
        }
        // CHECK IF DIRECTION IS LEFT
        if(dir.x == -1.0f && (int)position.x-1 >= 0) 
        {
            if(mazeGrid[(int)position.x-1,(int)position.y]){
                // Debug.Log("PACMAN MOVE LEFT");
                return Vector2.left;}
        }
        // CHECK IF DIRECTION IS RIGHT
        if(dir.x == 1.0f  && (int)position.x+1 < MazeSize)
        {
           if(mazeGrid[(int)position.x+1,(int)position.y]){
                // Debug.Log("PACMAN MOVE RIGHT");
                return Vector2.right;}
        }
        
        return result;
    }
    public void PrintArray()
    {
        
        string result="";
        for (int y = MazeSize-1; y >=0; y--)
        {
            for (int x = 0; x < mazeGrid.GetLength(0); x++)
            {
                if(mazeGrid[x,y])
                {
                    result +="O";
                    // CREATE PRIMITIVE
                    //  GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //  cube.transform.position = new Vector3(x-(int)MazeSize/2, y-(int)MazeSize/2, -4);
                }else{
                    result +="#";
                }
            }
        result += "\n";        
        }
        Debug.Log("Current Mazegrid: \n" + result);
    }
    
}
