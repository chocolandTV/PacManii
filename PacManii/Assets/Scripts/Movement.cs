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
    public Vector2 direction{get; set;}
    public Vector2 nextDirection{get;private set;}
    public GameObject mazeObject;
    private MazeGrid maze;
    private Rigidbody rigidbody;
    // FUNCTIONS"Movement.rigidbody" blendet den vererbten Member "Component.rigidbody" aus. Verwenden Sie das new-Schlüsselwort, wenn das 

    private void Start() {
        maze = mazeObject.GetComponent<MazeGrid>();
        rigidbody = GetComponent<Rigidbody>();
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
        Move();

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
        Vector2 position = this.rigidbody.position;
        Vector2 translate = this.direction * this.speed * this.speedMultiplier;
        SetDirection(this.direction);
        this.rigidbody.MovePosition(position + translate);
    }
    public void MoveFrightend()
    {

    }
}
