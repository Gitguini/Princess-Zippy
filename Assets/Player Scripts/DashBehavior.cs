using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : MonoBehaviour
{

    AudioManager sounds;

    InputManagement inputs;
    MovestateManagement movestate;
    PlayerMovement mover;
    CollisionManager collideThing;
    PlayerData playerData;
    PlayerVelocityControl velocity;

    LineRenderer ziplineRender;

    GameObject target;

    int dashCountdown;

    Vector3[] positions = new Vector3[2];


    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<InputManagement>();
        movestate = GetComponent<MovestateManagement>();
        mover = GetComponent<PlayerMovement>();
        collideThing = GetComponent<CollisionManager>();
        playerData = GetComponent<PlayerData>();
        velocity = GetComponent<PlayerVelocityControl>();

        ziplineRender = GetComponent<LineRenderer>();
        ziplineRender.useWorldSpace = true;
        ziplineRender.startColor = Color.black;
        ziplineRender.endColor = Color.black;

        GameObject audioGO = GameObject.Find("AudioManager");
        sounds = audioGO.GetComponent<AudioManager>();

        target = gameObject;

        dashCountdown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashCountdown > 0)
        {
            dashCountdown--;
        }
        if (dashCountdown == 1)
        {
            dashTo(target);
        }

        //set the zipline's start and end to always be at the player and target position
        if (target != null)
        {
            positions[0] = transform.position;
            positions[1] = target.transform.position;

            ziplineRender.SetPositions(positions);
        }


    }

    public void checkDash()
    {
        if (inputs.getDashInput())
        {
            if (dashCountdown == 0) //currently not dashing or mid-wait or whatever. Find the target, start the countdown if found.
            {
                target = findTarget();
                if (target != null)
                {
                    //start countdown and put us in dash state
                    dashCountdown = playerData.dashDelay;
                    movestate.setMovestate(movestate.DASHING);

                    sounds.playSound("prepareDash");

                    ziplineRender.startWidth = 0.25f;
                    ziplineRender.endWidth = 0.25f;

                }

            } else //dash countdown in progress + dash input received = dash-through
            {
                dashThrough(target);
            }
        }
    }

    public float objectSizeOffset = 2f;

    void dashTo(GameObject theThing)
    {
        dashCountdown = 0;
        ziplineRender.startWidth = 0f;
        ziplineRender.endWidth = 0f;

        sounds.playSound("dashTo");


        Vector2 targetVector = getVectorToTarget(theThing);

        Vector2 targetVectorNormalized = targetVector.normalized;

        Vector2 adjustedVector = targetVector - (targetVectorNormalized * objectSizeOffset);

        mover.move(adjustedVector);

        movestate.setMovestate(movestate.FALLING);

    }

    void dashThrough(GameObject theThing)
    {
        dashCountdown = 0;
        ziplineRender.startWidth = 0f;
        ziplineRender.endWidth = 0f;

        sounds.playSound("dashThrough");


        Vector2 targetVector = getVectorToTarget(theThing);

        Vector2 targetVectorNormalized = targetVector.normalized;

        Vector2 adjustedVector = targetVector + (targetVectorNormalized * objectSizeOffset);

        mover.move(adjustedVector);

        velocity.setVelocity(targetVectorNormalized * playerData.dashThroughSpeed);
        movestate.setMovestate(movestate.RISING);

    }

    Vector2 getVectorToTarget(GameObject theThing)
    {
        Vector2 playerPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 objectPosition = new Vector2(theThing.transform.position.x, theThing.transform.position.y);


        return (objectPosition - playerPosition);
    }
    

    GameObject findTarget()
    {
        //give collider.dashCollisionFind() the vector from the player position to the mouse position
        Vector3 playerPosition3D = gameObject.transform.position;
        Vector2 playerPosition2D = new Vector2(playerPosition3D.x, playerPosition3D.y);
        Vector2 playerToMouseVector = inputs.getMousePos() - playerPosition2D;

        GameObject newTarget = collideThing.dashCollisionFind(playerToMouseVector, playerToMouseVector.magnitude);

        return newTarget;
    }
}
