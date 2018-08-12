using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public void StartGame()
    {
        GameObject.FindObjectOfType<CircleManager>().StartGame();
    }
}
