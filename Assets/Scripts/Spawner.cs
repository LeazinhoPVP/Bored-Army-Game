using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float gameTimer;

    public Transform[] spawners;
    public float spawnRate;
    float spawnTimer;
    public GameObject[] enemysPrefab;

    public Text timerText;

    private void Start()
    {
        spawnRate = GameManager.Instance.spawnRate;
        gameTimer = GameManager.Instance.gameTimer * 60;
    }
    private void Update()
    {
        if(gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            DisplayTime(gameTimer);
        }
        else
        {
            gameTimer = 0;
            GameManager.Instance.GameOver();
        }



        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
            SpawnChaser();
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SpawnChaser()
    {
        int aux = Random.Range(0, 3);
        int e = Random.Range(0, enemysPrefab.Length);
        Instantiate(enemysPrefab[e], spawners[aux].position, spawners[aux].rotation);
        spawnTimer = 0;
    }
}
