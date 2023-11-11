using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [Header("Win Game Settings")]
    public BoxCollider c_exit;
    public static int GameProgState; //playing, winning, losing etc

    void OnTriggerEnter(Collider c_exit)
    {
        if (c_exit.CompareTag("Player"))
        {
            print("game over");
            GameProgState = 1;
        }
    }
}
