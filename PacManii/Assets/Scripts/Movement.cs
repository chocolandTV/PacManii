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
    private void ResetState()
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

        
        this.direction = maze.CheckNextMove(dir, this.rigidbody.position);
        this.nextDirection = Vector2.zero;


    }
    private void Move()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
        
    }
    public void MoveFrightend()
    {

    }
}
