using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneController : MonoBehaviour
{
    public void GameStartClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
