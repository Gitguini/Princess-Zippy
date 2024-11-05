using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private PlayerVelocityControl velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = GetComponent<PlayerVelocityControl>();
    }

    public void moveVel()
    {
        transform.Translate(velocity.getVelocity() * Time.deltaTime);
    }

    public void move(Vector2 input)
    {
        transform.Translate(input);
    }
}
