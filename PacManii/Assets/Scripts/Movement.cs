using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class Movement : MonoBehaviour
{
    // TRANSFORM

    public float speed = 1.0f;
    public float speedMultiplier = 2.0f;
    public Vector2 initialDirection;

    public Rigidbody rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; set; }
    public Vector3 startingPosition { get; private set; }
    public GameObject mazeObject;
    private MazeGrid maze;
    [field: SerializeField] private bool _drawGizmos= false;

    // FUNCTIONS"Movement.rigidbody" blendet den vererbten Member "Component.rigidbody" aus. Verwenden Sie das new-Schlüsselwort, wenn das 
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.startingPosition = this.transform.position;

    }
    private void Start()
    {
        maze = mazeObject.GetComponent<MazeGrid>();
        // INIT PLAYER / GHOST SET POSITION   
        ResetState();
    }
    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.startingPosition;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.enabled = true;
    }
    private void FixedUpdate()
    {
        Move();

    }
    private void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }
    private void SetDirection(Vector2 dir)
    {

        
        // this.direction = maze.CheckNextMove(dir, this.rigidbody.position);
        this.direction = this.nextDirection;
        this.nextDirection = Vector2.zero;


    }
    public Vector2 GridPosition()
    {
        return maze.GridPosition(this.gameObject.transform.position);
    }
  
    private void Move()
    {
        // Vector3Int posi = new Vector3Int((int)this.rigidbody.position.x,(int)this.rigidbody.position.y, (int)this.rigidbody.position.z);
        // Debug.Log("X: " + posi.x + " Y: " + posi.y);
        // maze.drawGizimos(1, this.rigidbody.position);
        if(maze.CheckIfDirValid(this.direction, this.rigidbody.position))
        {
            
            Vector2 position = this.rigidbody.position;
            // Vector2 position = new Vector2(posi.x, posi.y);
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(position + translation);
            if(this.direction.y !=0)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y, transform.position.z);
            }
            if(this.direction.x !=0)
            {
                transform.position = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y), transform.position.z);
            }
        }

    }
    
    private void OnDrawGizmos() {
        if(_drawGizmos)
        {
            if(maze != null && maze.mazeGrid != null)
            {
                // CUTPOSITION
                Vector3 cutPosition = new Vector3 (Mathf.RoundToInt(rigidbody.position.x), Mathf.RoundToInt(rigidbody.position.y),-2);
                // CHECK UP
                if(maze.CheckIfDirValid(Vector2.up,this.rigidbody.position))
                    Gizmos.color=Color.green;
                else
                    Gizmos.color= Color.red;
                
                Gizmos.DrawCube(cutPosition + Vector3.up,Vector3.one*0.5f);
                    
                // CHECK DOWN
                if(maze.CheckIfDirValid(Vector2.down,this.rigidbody.position))
                    Gizmos.color=Color.green;
                else
                    Gizmos.color= Color.red;
                
                Gizmos.DrawCube(cutPosition + Vector3.down,Vector3.one*0.5f);
                // CHECK LEFT
                if(maze.CheckIfDirValid(Vector2.left,this.rigidbody.position))
                    Gizmos.color=Color.green;
                else
                    Gizmos.color= Color.red;
                
                Gizmos.DrawCube(cutPosition + Vector3.left,Vector3.one*0.5f);
                // CHECK RIGHT
                if(maze.CheckIfDirValid(Vector2.right,this.rigidbody.position))
                    Gizmos.color=Color.green;
                else
                    Gizmos.color= Color.red;
                
                Gizmos.DrawCube(cutPosition + Vector3.right,Vector3.one*0.5f);

                // SHOW OWN POSITION
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(cutPosition, Vector3.one*0.5f);
                // SHOW OFFSET POSITION
                Gizmos.color  = Color.blue;
                Vector3 offsetPos = rigidbody.position + new Vector3((int)maze.MazeSize/2,(int)maze.MazeSize/2,0);
                Gizmos.DrawCube(offsetPos, Vector3.one * 0.5f);
            }
        }
    }
}
