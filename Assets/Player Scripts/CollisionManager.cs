using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    PlayerVelocityControl velocity;

    public int RIGHT = 0;
    public int LEFT = 1;
    public int UP = 2;
    public int DOWN = 3;
    public int NONE = 4;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private RaycastHit2D[] groundBuffer = new RaycastHit2D[16];
    private RaycastHit2D[] dashCollideBuffer = new RaycastHit2D[16];
    private Rigidbody2D body;
    private ContactFilter2D contactFilter;
    private ContactFilter2D dashContactFilter;
    private int collisionCount;
    private int groundCollideCount;
    private int dashCollideCount;


    // Start is called before the first frame update
    void Start()
    {
        velocity = GetComponent<PlayerVelocityControl>();

        body = GetComponent<Rigidbody2D>();

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        dashContactFilter.useTriggers = true;
        dashContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(7)); //dash object layer
        dashContactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        goodOldWallHitMethod();
    }

    void OnCollisionStay2D(Collision2D info)
    {
        foreach(ContactPoint2D contact in info.contacts)
        {
            transform.Translate(contact.normal * 0.01f);
        }
    }

    void goodOldWallHitMethod()
    {
        int hitDir = wallHit(velocity.getVelocity(), velocity.getVelocity().magnitude * Time.deltaTime);
        if (hitDir == RIGHT || hitDir == LEFT)
        {
            Vector2 newVel = new Vector2(velocity.getXVel(), velocity.getYVel());
            newVel.x = 0;
            velocity.setVelocity(newVel);
        }
    }

    public bool onSolidGround()
    {
        groundCollideCount = body.Cast(Vector2.down, groundBuffer, 0.05f);
        if (groundCollideCount < 1) 
        { 
            return false; 
        } else
        {
            return true;
        }
    }


    public GameObject dashCollisionFind(Vector2 direction, float distance)
    {
        dashCollideCount = body.Cast(direction, dashContactFilter, dashCollideBuffer, distance);


        if (dashCollideCount < 1)
        {
            return null;
        }
        else
        {
            GameObject foundTarget = null;
            RaycastHit2D thisHit = dashCollideBuffer[0];
            for (int i = 0; i < dashCollideCount; i++)
            {
                thisHit = dashCollideBuffer[i];
                
            }

            foundTarget = thisHit.collider.gameObject;
            return foundTarget;
        }
    }

    //returns (in int form) whether the given vector causes a cast of body to hit a wall/ceiling/floor.
    public int wallHit(Vector2 direction, float distance)
    {
        collisionCount = body.Cast(direction, hitBuffer, distance);

        if (collisionCount < 1)
        {
            return NONE;
        }

        int normalDirection = NONE;

        for (int i = 0; i < collisionCount; i++)
        {
            //loop through array, get normal, determine its direction, assign some int variable correspondingly.
            RaycastHit2D thisHit = hitBuffer[i];
            Vector2 thisNormal = thisHit.normal;

            if (Vector2.Angle(Vector2.left, thisNormal) < 10)
            {
                normalDirection = LEFT;
            } else if (Vector2.Angle(Vector2.up, thisNormal) < 10)
            {
                normalDirection = UP;
            } else if (Vector2.Angle(Vector2.down, thisNormal) < 10)
            {
                normalDirection = DOWN;
            } else if (Vector2.Angle(Vector2.right, thisNormal) < 10)
            {
                normalDirection = RIGHT;
            }
            
            
        }
        //re
        return normalDirection;
    }
    
}
