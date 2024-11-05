using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralPlayerControl : MonoBehaviour
{
    private InputManagement inputs; //contains method for collecting inputs and stores what the inputs currently are

    private PlayerVelocityControl velocity; //contains method for calculating velocity, stores current velocity

    private PlayerMovement mover; //contains method for actually moving the player, using PlayerData's information

    private DashBehavior dasher; //contains dash behavior including cooldowns and whether dash is ready, as well as enacting dash when it occurs

    private MovestateManagement movestate; //contains method for deciding movestate, stores current movestate

    private PlayerData playerData; //contains all "Tweakable" variables and anything else that needs to be stored

    private JumpBehavior jumper;

    private Vector2 currentVelocity;

    void Start()
    {
        inputs = GetComponent<InputManagement>();
        velocity = GetComponent<PlayerVelocityControl>();
        mover = GetComponent<PlayerMovement>();
        dasher = GetComponent<DashBehavior>();
        movestate = GetComponent<MovestateManagement>();
        playerData = GetComponent<PlayerData>();
        jumper = GetComponent<JumpBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        mover.moveVel();
    }

    void FixedUpdate()
    {
        inputs.getNewInput(); //updates all values in inputs. Contains up-to-date info about player inputs.
        dasher.checkDash();
        //slasher.checkSlash(inputs.getSlashInput());
        jumper.checkJump();
        movestate.updateMoveState();
        currentVelocity = velocity.calculateVelocity();
        

    }
}
