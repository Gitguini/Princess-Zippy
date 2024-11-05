using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float voluntaryTopSpeed = 5f;     //past this horizontal velocity, inputs which Increase speed don't work
    public float voluntaryTopSpeedUpperThreshold = 7f; //past this, on the ground, will drastically decelerate.
    public float groundAcceleration = 5f;    //speed of acceleration on ground (should be high. Might want to split into Start and Stop)
    public float groundDecelerationVoluntary = 10f; //speed of stopping when a movement key is held in reverse of the current move
    public float groundDecelerationSkid = 20f;//speed of stopping when exceeding the topspeed Upper Threshold
    public float speedDeadZone = 3f;         //when no movement key is held and moving slower than this, instantly stop

    public float upGravity = 5f;             //acceleration downwards while ascending
    public float downGravity = 5f;           //acceleration downwards while descending
    public float maxFallSpeed = -10f;        //past this vertical speed, do not accelerate downwards

    public float airAcceleration = 5f;       //speed of acceleration from movement keys in air (lower than on ground)

    public float jumpStrength = 10;          //upwards speed which a jump accelerates you to
    public float sideJumpStrength = 4;       //sideways speed which a jump accelerates you to

    public int numAirJumps = 1;

    public int dashDelay = 16;

    public float dashThroughSpeed = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
