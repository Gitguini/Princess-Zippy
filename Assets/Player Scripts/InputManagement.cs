using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    private PlayerData playerData;
    private MovestateManagement movestate;

    private float movementInput = 0f;
    private bool jumpInput = false;
    private bool slashInput = false;
    private bool dashInput = false;

    private bool dashInputBuff = false;
    private bool jumpInputBuff = false;

    private Vector2 mousePos = new Vector2(0,0);
    private Vector2 mousePosWorld = new Vector2(0,0);

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        movestate = GetComponent<MovestateManagement>();
        playerData = GetComponent<PlayerData>();
    }



    //gets called once per frame in CentralPlayerControl. Used to update current input values
    public void getNewInput()
    {
        //updates all input values based on what they currently are and input from the player

        movementInput = Input.GetAxis("Horizontal");
        jumpInput = (Input.GetAxis("Jump") > 0);
        slashInput = (Input.GetAxis("Fire1") > 0);
        dashInput = (Input.GetAxis("Fire2") > 0);

        //if, the last time this method was called, dash or jump input was on, ignore dash or jump input (so it activates for 1 frame only)
        if (dashInput)
        {
            if (dashInputBuff)
            {
                dashInput = false;
            }
            else
            {
                dashInputBuff = true;
            }
        }
        else
        {
            dashInputBuff = false;
        }

        if (jumpInput)
        {
            if (jumpInputBuff)
            {
                jumpInput = false;
            }
            else
            {
                jumpInputBuff = true;
            }
        }
        else
        {
            jumpInputBuff = false;
        }

        if (slashInput || dashInput)
        {
            mousePos = Input.mousePosition;
            float camZPos = cam.transform.position.z;
            Vector3 mousePosWorld3 = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosWorld.x = mousePosWorld3.x;
            mousePosWorld.y = mousePosWorld3.y;
        }

    }

    //and now, a series of getters for various inputs
    public float getMovementInput()
    {
        return movementInput;
    }

    public bool getJumpInput()
    {
        return jumpInput;
    }
    
    public bool getSlashInput()
    {
        return slashInput;
    }

    public bool getDashInput()
    {
        return dashInput;
    }

    public Vector2 getMousePos()
    {
        return mousePosWorld;
    }
    
}
