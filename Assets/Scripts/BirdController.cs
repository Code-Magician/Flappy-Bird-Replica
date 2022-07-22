using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float upThrust = 10f;
    private Rigidbody2D rb;
    private Animator birdAnim;
    private B_and_ObstacleController mainScript;
    bool initialThrust;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        birdAnim = GetComponent<Animator>();
        birdAnim.enabled = false;
        mainScript = Camera.main.gameObject.GetComponent<B_and_ObstacleController>();
        mainScript.birdPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainScript.gameStarted)
        {
            birdAnim.enabled = true;
            rb.gravityScale = 10;

            if (!initialThrust)
            {
                initialThrust = true;
                rb.velocity = Vector2.up * upThrust;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                rb.velocity = Vector2.up * upThrust;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                mainScript.gameStarted = false;
                SceneManager.LoadScene("Gameplay");
            }
        }

        // GameOver
        PlayerOutOfScreen();
    }

    private void PlayerOutOfScreen()
    {
        if ((transform.position.y <= (mainScript.screenBottom + 2f)) || (transform.position.y >= (mainScript.screenTop - 2f)))
        {
            mainScript.gameOver = true;
            mainScript.gameStartPanel.SetActive(true);
            mainScript.scorePanel.SetActive(false);
            mainScript.gameStartText.text = "Game Over!!";
            Time.timeScale = 0;
        }
    }

}
