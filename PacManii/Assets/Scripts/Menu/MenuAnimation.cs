using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    private Vector3 startPosition;
    private float maxscrollLenght= 21.0f;
    public float speed =5.0f;
    // Update is called once per frame
    private void Start() {
        startPosition=  this.gameObject.transform.position;
    }
    void Update()
    {
        Scroll();
        if(this.gameObject.transform.position.x > maxscrollLenght)
        {
            ObjectReset();
        }
    }
    private void Scroll()
    {
        this.transform.position+= Vector3.right*speed*Time.deltaTime;
    }
    private void ObjectReset()
    {
        this.transform.position = startPosition;
    }
}
