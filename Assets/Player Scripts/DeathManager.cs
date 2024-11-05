using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{

    GameObject respawnAnchor;

    // Start is called before the first frame update
    void Start()
    {
        respawnAnchor = GameObject.Find("PrincessSpawnPoint");
    }

    void respawn()
    {
        transform.position = respawnAnchor.transform.position;
    }

    void Update()
    {

        if (transform.position.y < -20)
        {
            respawn();
        }
    }

}
