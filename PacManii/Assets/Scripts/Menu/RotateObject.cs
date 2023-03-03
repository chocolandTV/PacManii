using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 50.0f;
    // Start is called before the first frame update
    private void RotateGameObject ()
    {
        transform.Rotate (Vector3.down * speed * Time.deltaTime, Space.World);
    }
    // Update is called once per frame
    void Update()
    {
        RotateGameObject();
    }
}
