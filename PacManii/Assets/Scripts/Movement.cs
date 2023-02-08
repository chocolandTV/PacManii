using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // TRANSFORM
    public Transform body {get; private set;}
    public float speed = 1.0f;
    public float speedMultiplier = 2.0f;

    public Vector2 startPosition;
    public Vector2 direction{get; private set;}
    public Vector2 nextDirection{get;private set;}
    public GameObject mazeObject;
    private MazeGrid maze;
    // FUNCTIONS

    private void Start() {
        maze = mazeObject.GetComponent<MazeGrid>();
        // INIT PLAYER / GHOST SET POSITION   
        ResetState();
    }
    private void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.startPosition;
        this.nextDirection=Vector2.zero;
        this.transform.position = this.startPosition;
        this.enabled = true;
    }
    private void FixedUpdate() {
        // Move ()

    }
    private void SetDirection(Vector2 dir)
    {
        if(maze.CheckMoveNext(dir, nextDirection))
        {
            // CHECKED IF NEXT POSITION IS NO WALL
            // this.direction
            this.direction = dir;
            this.nextDirection = Vector2.zero;

        }
        else{
            this.nextDirection = direction;
        }
    }
    private void Move()
    {

    }

}
