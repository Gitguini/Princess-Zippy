using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleSuccess : MonoBehaviour
{
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;




    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.CompareTag("Princess"))
        {
            levelWin();
        }
    }

    void levelWin()
    {
        if (sceneName == "Level1")
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
