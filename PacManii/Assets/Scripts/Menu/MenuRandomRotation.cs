using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRandomRotation : MonoBehaviour
{
    [SerializeField]private GameObject gameManagerObject;
    private GameManager gameManager;
    private Quaternion _targetRotation;
    private float timestep  = 10.0f;
    private float timeSpend = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        
        // _targetRotation = randomRot();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0,gameManager.MenuTurningSpeed,0), Space.World);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, gameManager.MenuTurningSpeed * Time.deltaTime);
        
        if(Time.time - timeSpend > timestep)
        {
            timeSpend = Time.time;
            gameManager.MenuTurningSpeed =0.0f;
        }
    }
    // private Quaternion randomRot()
    // {
    //     gameManager.MenuTurningSpeed = 10f;
    //     return (Random.rotation);
    // }
    
}
