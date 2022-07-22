using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    B_and_ObstacleController mainScript;
    Vector3 upperRightCorner;
    private bool scoreAdded;

    private void Awake()
    {
        mainScript = Camera.main.gameObject.GetComponent<B_and_ObstacleController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= (mainScript.screenLeft * 1.5f))
            Destroy(gameObject);
        else
            transform.Translate(Vector3.left * mainScript.obstacleSpeed * Time.deltaTime);

        if ((transform.position.x <= (mainScript.birdPositionX - 5f)) && !scoreAdded)
        {
            scoreAdded = true;
            mainScript.score++;
            mainScript.scoreText.text = "Score : " + mainScript.score.ToString("00");
            // Debug.Log("Score : " + mainScript.score);
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        if (mainScript.score % 10 == 0)
        {
            if (mainScript.obstacleSpeed <= 100f)
            {
                mainScript.obstacleSpeed += 5;
                mainScript.bgSpeed += 1;
            }
        }
    }
}
