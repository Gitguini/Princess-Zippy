using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 position;

    private Vector3 newPos;

    private float camX;
    private float camY;
    private float playerX;
    private float playerY;

    private Vector2 what;
    private Vector2 who;
    private Vector2 between;


    //the camera's speed will be proportional to its distance to the player and this constant:
    public float speedCoefficient = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Princess");
        
        what = new Vector2(0, 0);
        who = new Vector2(0, 0);

        position = transform.position;
        newPos = new Vector3(0, 0, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        position = transform.position;

        camX = position.x;
        camY = position.y;
        playerX = playerPos.x;
        playerY = playerPos.y;

        //2D vectors: better to do vector math with because in 3D, the camera is offset on the z-axis to see things
        what.x = camX;
        what.y = camY;
        who.x = playerX;
        who.y = playerY;

        between = who - what; //the vector Between cam and player


        //alter this to function as a velocity vector by multiplying by speedCoefficient
        between = between * speedCoefficient;

        //now turn it back into a Vector3
        newPos.x = between.x;
        newPos.y = between.y;

        //now translate camera position by this velocity vector:
        transform.Translate(between * Time.deltaTime);
        

    }
}
