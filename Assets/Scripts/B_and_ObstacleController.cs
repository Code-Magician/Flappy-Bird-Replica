using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class B_and_ObstacleController : MonoBehaviour
{
    public bool gameStarted;
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    public float halfWidth;
    private Vector2 bg1_startPos;
    private Vector2 bg2_startPos;

    public int bgSpeed = 20;
    public int obstacleSpeed = 30;

    [SerializeField] GameObject ObstaclePrefab;
    private Vector2 spawnPos;
    [SerializeField] GameObject spawnPostionGameObject;
    public bool gameOver;
    public float lowerLimit = 1.5f;
    public float upperLimit = 2f;

    public TextMeshProUGUI gameStartText;
    public GameObject gameStartPanel;

    string[] instructions = { "Ready!!", "Game starts in : 3", "Game starts in : 2", "Game starts in : 1", "Play" };
    bool obstacleSpawningStarted;

    public float screenHeight;
    public float screenWidth;
    public float screenLeft;
    public float screenRight;
    public float screenTop;
    public float screenBottom;

    public int score = 0;
    public float birdPositionX;

    public TextMeshProUGUI scoreText;
    public GameObject scorePanel;

    private void Awake()
    {
        Time.timeScale = 1;
        CalculateScreenEdgePostions();
    }
    void Start()
    {
        bg1_startPos = bg1.transform.position;
        bg2_startPos = bg2.transform.position;

        halfWidth = bg2.GetComponent<BoxCollider2D>().size.x * bg2.transform.localScale.x;

        spawnPos = spawnPostionGameObject.transform.position;

        StartCoroutine(StartInstructionPlay());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (!obstacleSpawningStarted)
            {
                obstacleSpawningStarted = true;
                StartCoroutine(SpawnObstacles());
            }

            if (bg1.transform.position.x <= (bg1_startPos.x - halfWidth))
            {
                bg1.transform.position = bg1_startPos;
                bg2.transform.position = bg2_startPos;
            }
            else
            {
                bg1.transform.Translate(Vector2.left * bgSpeed * Time.deltaTime);
                bg2.transform.Translate(Vector2.left * bgSpeed * Time.deltaTime);
            }
        }

        CheckScreenSizeChanged();
    }

    IEnumerator SpawnObstacles()
    {
        while (!gameOver)
        {
            float verticleOffset = Random.Range(-10f, 10f);
            Vector2 verticleOffsetVect = new Vector2(0, verticleOffset);

            float upperObstacleVerticleOffset = Random.Range(0f, 2.5f);
            float lowerObstacleVerticleOffset = Random.Range(0f, 2.5f);

            GameObject tempObj = Instantiate(ObstaclePrefab, spawnPos + verticleOffsetVect, Quaternion.identity);

            GameObject up = tempObj.transform.GetChild(1).gameObject;
            GameObject down = tempObj.transform.GetChild(0).gameObject;

            up.transform.localPosition = new Vector3(up.transform.localPosition.x, up.transform.localPosition.y - upperObstacleVerticleOffset);
            down.transform.localPosition = new Vector3(down.transform.localPosition.x, down.transform.localPosition.y + lowerObstacleVerticleOffset);

            float horizontalOffset = Random.Range(lowerLimit, upperLimit);
            // Debug.Log(horizontalOffset);
            yield return new WaitForSeconds(horizontalOffset);
        }
    }

    IEnumerator StartInstructionPlay()
    {
        int i = 0;
        while (i != instructions.Length)
        {
            gameStartText.text = instructions[i];
            yield return new WaitForSeconds(1f);
            i++;
        }

        gameStartPanel.SetActive(false);
        gameStarted = true;
        scorePanel.SetActive(true);
    }

    private void CalculateScreenEdgePostions()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);

        lowerLeftCornerScreen = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        upperRightCornerScreen = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        screenLeft = lowerLeftCornerScreen.x;
        screenBottom = lowerLeftCornerScreen.y;
        screenTop = upperRightCornerScreen.y;
        screenRight = upperRightCornerScreen.x;
        Debug.Log("Screen Left : " + screenLeft);
    }

    private void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width ||
            screenHeight != Screen.height)
        {
            CalculateScreenEdgePostions();
        }
    }

}
