using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ? instance : FindObjectOfType<GameManager>();

    public int points;
    public GameObject player;

    public AudioSource audioSource;

    public float gameTimer;
    public float spawnRate;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddPoints(int p)
    {
        points += p;
        Debug.Log(points);
        audioSource = GetComponent<AudioSource>();
    }
    public void ClearScore()
    {
        points = 0;
    }
    public void PlayAudio(AudioClip a, float v, float p)
    {
        audioSource.pitch = p;
        audioSource.PlayOneShot(a, v);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
