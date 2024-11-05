using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    AudioManager sounds;


    InputManagement inputs;
    PlayerVelocityControl velocity;
    MovestateManagement movestate;
    PlayerData data;

    private int numAirJumpsRemaining = 0;


    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<InputManagement>();
        velocity = GetComponent<PlayerVelocityControl>();
        movestate = GetComponent<MovestateManagement>();
        data = GetComponent<PlayerData>();

        GameObject audioGO = GameObject.Find("AudioManager");
        sounds = audioGO.GetComponent<AudioManager>();

    }

    void Update()
    {
        if (movestate.getMovestate() == movestate.GROUNDED)
        {
            numAirJumpsRemaining = data.numAirJumps;
        }

    }

    public void checkJump()
    {

        if (inputs.getJumpInput())
        {

            if (movestate.getMovestate() == movestate.GROUNDED)
            {

                jump();

            }
            else if (movestate.getMovestate() == movestate.FALLING || movestate.getMovestate() == movestate.RISING)
            {

                if (numAirJumpsRemaining > 0)
                {
                    airJump();
                }

            }

        }
    }

    void jump()
    {

        sounds.playSound("jump");

        movestate.setMovestate(movestate.RISING);
        Vector2 newVel = velocity.getVelocity();
        newVel.y = data.jumpStrength;
        velocity.setVelocity(newVel);
    }

    void airJump()
    {

        sounds.playSound("jump");

        numAirJumpsRemaining--;

        movestate.setMovestate(movestate.RISING);
        Vector2 newVel = velocity.getVelocity();
        newVel.y = data.jumpStrength;
        if (inputs.getMovementInput() > 0.1f && velocity.getXVel() > data.sideJumpStrength)
        {
            newVel.x = data.sideJumpStrength;
        } else if (inputs.getMovementInput() < -0.1f && velocity.getXVel() < (-1 * data.sideJumpStrength))
        {
            newVel.x = -data.sideJumpStrength;
        }
        velocity.setVelocity(newVel);
    }
}
