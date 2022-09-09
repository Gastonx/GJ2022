using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("log " + collision.tag + " " + collision.name);
        if (collision.tag == "Player")
        {
            GameManager.Instance.gameState = GameState.OUTRO;
            Debug.Log("outro");
        }
    }
}
