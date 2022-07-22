using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    B_and_ObstacleController mainScript;

    private void Awake()
    {
        mainScript = Camera.main.GetComponent<B_and_ObstacleController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mainScript.gameOver = true;
        mainScript.gameStartPanel.SetActive(true);
        mainScript.scorePanel.SetActive(false);
        mainScript.gameStartText.text = "Game Over!!";
        Time.timeScale = 0;
    }
}
