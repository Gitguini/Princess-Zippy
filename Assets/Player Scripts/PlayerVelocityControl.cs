using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityControl : MonoBehaviour
{
    private Vector2 currentVelocity = new Vector2(0,0);

    private MovestateManagement movestate;
    private InputManagement inputs;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        movestate = GetComponent<MovestateManagement>();
        inputs = GetComponent<InputManagement>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 calculateVelocity()
    {

        if (movestate.getMovestate() == movestate.GROUNDED)
        {
            //cancel downwards momentum
            if (currentVelocity.y < 0)
            {
                currentVelocity.y = 0;
            }

            if (currentVelocity.x >= playerData.voluntaryTopSpeedUpperThreshold) //we were accelerated somehow to be Real Fast. slow down very quick
            {

                currentVelocity.x -= playerData.groundDecelerationSkid * Time.deltaTime;

            }
            else if (currentVelocity.x <= (-1 * playerData.voluntaryTopSpeedUpperThreshold))
            {
                currentVelocity.x += playerData.groundDecelerationSkid * Time.deltaTime;
            }
            else if (inputs.getMovementInput() < 0) //attempting to go left
            {

                if (currentVelocity.x > 0)
                {
                    currentVelocity.x -= playerData.groundDecelerationVoluntary * Time.deltaTime; // decelerate from right-movement
                }
                else if (currentVelocity.x > (-1 * playerData.voluntaryTopSpeed))
                {
                    // we are not beyond the top speed, go ahead and accelerate
                    currentVelocity.x -= playerData.groundAcceleration * Time.deltaTime;
                }

            }
            else if (inputs.getMovementInput() > 0) //attempting to go right
            {

                if (currentVelocity.x < 0)
                {
                    currentVelocity.x += playerData.groundDecelerationVoluntary * Time.deltaTime; //decelerate from left-movement
                }
                else if (currentVelocity.x < playerData.voluntaryTopSpeed)
                {
                    //again, not beyond top speed, accelerate right
                    currentVelocity.x += playerData.groundAcceleration * Time.deltaTime;
                }

            }
            else //no movement input, decelerate
            {
                if (currentVelocity.x < -0.01f) //if moving left, decelerate right
                {
                    currentVelocity.x += playerData.groundDecelerationVoluntary * Time.deltaTime;
                }
                else if (currentVelocity.x > 0.01f) //if moving right, decelerate left
                {
                    currentVelocity.x -= playerData.groundDecelerationVoluntary * Time.deltaTime;
                }

                //once below a certain threshold of speed, stop
                if (currentVelocity.x < playerData.speedDeadZone && currentVelocity.x > -1 * playerData.speedDeadZone)
                {
                    currentVelocity.x = 0f;
                }
            }


        } else if (movestate.getMovestate() == movestate.RISING)
        {

            if (currentVelocity.y > playerData.maxFallSpeed)
            {
                currentVelocity.y -= playerData.upGravity * Time.deltaTime;
            }

            if (inputs.getMovementInput() < 0 && currentVelocity.x > -1 * playerData.voluntaryTopSpeed) //left
            {
                currentVelocity.x -= playerData.airAcceleration * Time.deltaTime;
            }
            else if (inputs.getMovementInput() > 0 && currentVelocity.x < playerData.voluntaryTopSpeed) //right
            {
                currentVelocity.x += playerData.airAcceleration * Time.deltaTime;
            }

        } else if (movestate.getMovestate() == movestate.FALLING)
        {

            if (currentVelocity.y > playerData.maxFallSpeed)
            {
                currentVelocity.y -= playerData.downGravity * Time.deltaTime;
            }

            if (inputs.getMovementInput() < 0 && currentVelocity.x > -1 * playerData.voluntaryTopSpeed) //left
            {
                currentVelocity.x -= playerData.airAcceleration * Time.deltaTime;
            }
            else if (inputs.getMovementInput() > 0 && currentVelocity.x < playerData.voluntaryTopSpeed) //right
            {
                currentVelocity.x += playerData.airAcceleration * Time.deltaTime;
            }

        } else if (movestate.getMovestate() == movestate.DASHING)
        {
            currentVelocity.x = 0;
            currentVelocity.y = 0;
        } else if (movestate.getMovestate() == movestate.SLASHING) {
            currentVelocity.x = 0;
            currentVelocity.y = 0;
        }

        /*
        //this method is called (in Update) once per frame and UPDATES OUR VELOCITY ACCORDING TO SEVERAL FACTORS:
        // - our current velocity
        // - our state of being {
        //              grounded = cancel downwards momentum and don't do gravity and push upwards if in a collider.
        //                  Use input movement with ground acceleration
        //                  if beyond voluntaryMaxSpeed, decelerate quickly
        //              rising = change velocity by upwards-gravity.
        //                  Use input movement with in-air acceleration
        //              falling = change velocity by downwards-gravity
        //                  Use input movement with in-air acceleration
        //              dashing = do not move under any circumstance
        //              jumping = instantly set y-velocity to a fixed value.
        //                  X velocity is unchanged if no x-input or one of 2 fixed values if there is x input.
        //                  immediately set jumping to false
        //              slashing = drastically decelerate. Gravity?? hmmm.
        //   }
        if (motionState == GROUNDED)
        {
            //cancel downwards momentum
            if (currentVelocity.y < 0)
            {
                currentVelocity.y = 0;
            }

            //if beyond max-velocity-upper-threshold in either direction, drastically decelerate.
            //otherwise, if movement input is left,
            // if movement is right, very quickly stop
            // if currently at max-voluntary-speed, do nothing at all
            // otherwise, accelerate left at groundAcceleration
            //if movement input is right,
            // if movement is left, very quickly stop
            // if currently at max-voluntary-speed, do nothing at all
            // otherwise, accelerate right at groundAcceleration
            if (currentVelocity.x >= voluntaryTopSpeedUpperThreshold) //we were accelerated somehow to be Real Fast. slow down very quick
            {

                currentVelocity.x -= groundDecelerationSkid * Time.deltaTime;

            }
            else if (currentVelocity.x <= (-1 * voluntaryTopSpeedUpperThreshold))
            {
                currentVelocity.x += groundDecelerationSkid * Time.deltaTime;
            }
            else if (movementInput < 0) //attempting to go left
            {

                if (currentVelocity.x > 0)
                {
                    currentVelocity.x -= groundDecelerationVoluntary * Time.deltaTime; // decelerate from right-movement
                }
                else if (currentVelocity.x > (-1 * voluntaryTopSpeed))
                {
                    // we are not beyond the top speed, go ahead and accelerate
                    currentVelocity.x -= groundAcceleration * Time.deltaTime;
                }

            }
            else if (movementInput > 0) //attempting to go right
            {

                if (currentVelocity.x < 0)
                {
                    currentVelocity.x += groundDecelerationVoluntary * Time.deltaTime; //decelerate from left-movement
                }
                else if (currentVelocity.x < voluntaryTopSpeed)
                {
                    //again, not beyond top speed, accelerate right
                    currentVelocity.x += groundAcceleration * Time.deltaTime;
                }

            }
            else //no movement input, decelerate
            {
                if (currentVelocity.x < -0.01f) //if moving left, decelerate right
                {
                    currentVelocity.x += groundDecelerationVoluntary * Time.deltaTime;
                }
                else if (currentVelocity.x > 0.01f) //if moving right, decelerate left
                {
                    currentVelocity.x -= groundDecelerationVoluntary * Time.deltaTime;
                }

                //once below a certain threshold of speed, stop
                if (currentVelocity.x < speedDeadZone && currentVelocity.x > -1 * speedDeadZone)
                {
                    currentVelocity.x = 0f;
                }
            }
        }
        else if (motionState == RISING)
        {
            if (currentVelocity.y > maxFallSpeed)
            {
                currentVelocity.y -= upGravity * Time.deltaTime;
            }

            if (movementInput < 0 && currentVelocity.x > -1 * voluntaryTopSpeed) //left
            {
                currentVelocity.x -= airAcceleration * Time.deltaTime;
            }
            else if (movementInput > 0 && currentVelocity.x < voluntaryTopSpeed) //right
            {
                currentVelocity.x += airAcceleration * Time.deltaTime;
            }

        }
        else if (motionState == FALLING)
        {
            if (currentVelocity.y > maxFallSpeed)
            {
                currentVelocity.y -= downGravity * Time.deltaTime;
            }

            if (movementInput < 0 && currentVelocity.x > -1 * voluntaryTopSpeed) //left
            {
                currentVelocity.x -= airAcceleration * Time.deltaTime;
            }
            else if (movementInput > 0 && currentVelocity.x < voluntaryTopSpeed) //right
            {
                currentVelocity.x += airAcceleration * Time.deltaTime;
            }
        }
        else if (motionState == DASHING)
        {

        }
        else if (motionState == SLASHING)
        {

        }
        else
        {
            Debug.Log("Something has gone horribly wrong. Illegal motionState");
        }
        */

        return new Vector2();
    }

    public Vector2 getVelocity()
    {
        return currentVelocity;
    }

    public void setVelocity(Vector2 input)
    {
        currentVelocity = input;
    }

    public float getYVel()
    {
        return currentVelocity.y;
    }

    public float getXVel()
    {
        return currentVelocity.x;
    }
}
