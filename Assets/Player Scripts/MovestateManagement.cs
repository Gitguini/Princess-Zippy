using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovestateManagement : MonoBehaviour
{
    private PlayerData playerData;
    private InputManagement inputs;
    private PlayerVelocityControl velocity;
    private CollisionManager collisionManager;


    //okay. How am i going to store the movestate
    //will it be a series of ints
    //will it be an enum?
    //...oh the great part is it doesn't even matter
    //here's how the average interaction with MovestateManagement is gonna look:
    //if (movestate.getMovestate == movestate.GROUNDED) { ... }
    //those two things will ALWAYS be of the same type because they're both from here
    //oh god why was i doing any of what i was doing before
    //i can do a series of ints now and fix it later if i want

    public int GROUNDED = 0;
    public int RISING = 1;
    public int FALLING = 2;
    public int DASHING = 3;
    public int SLASHING = 4;

    private int movestate = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GetComponent<PlayerData>();
        inputs = GetComponent<InputManagement>();
        velocity = GetComponent<PlayerVelocityControl>();
        collisionManager = GetComponent<CollisionManager>();
    }

    public void updateMoveState()
    {
        if (movestate == GROUNDED)
        {
            //where are we going to store collision?
            if (collisionManager.onSolidGround() == false) {
                movestate = FALLING;
            }
        } else if (movestate == RISING)
        {
            if(velocity.getYVel() < 0)
            {
                movestate = FALLING;
            }
        } else if (movestate == FALLING)
        {
            //if (collisionManager.wallHit(currentvelocity, currentvelocity.magnitude) == collisionManager.UP) {
            //movestate = GROUNDED;
            //}

            if(collisionManager.wallHit(velocity.getVelocity(), velocity.getVelocity().magnitude * Time.deltaTime) == collisionManager.UP)
            {
                movestate = GROUNDED;
            }

            if (velocity.getYVel() > 0)
            {
                movestate = RISING;
            }
        } else if (movestate == DASHING)
        {

        } else if (movestate == SLASHING)
        {

        }

    }

    public int getMovestate()
    {
        return movestate;
    }

    public void setMovestate(int input)
    {
        movestate = input;
    }
}
