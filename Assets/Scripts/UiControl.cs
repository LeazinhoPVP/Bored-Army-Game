using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiControl : MonoBehaviour
{
    public GameObject optionsAssets;
    public Slider timerSlider;
    public TextMeshProUGUI timeText;
    public Slider rateSlider;
    public TextMeshProUGUI rateText;

    public GameObject menuAssets;

    public TextMeshProUGUI pointsText;

    private void Update()
    {
        
    }
    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "EndGame")
        {
            pointsText.text = "Points: " + GameManager.Instance.points.ToString();
        }

    }
    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ClearScore();
        
    }
    public void OpenOpcs()
    {
        optionsAssets.SetActive(true);
        menuAssets.SetActive(false);
        UpdateSliders();
        rateSlider.value = GameManager.Instance.spawnRate;
        timerSlider.value = GameManager.Instance.gameTimer;
        Debug.Log("Opcoes abertas");
    }
    public void CloseOpcs()
    {
        optionsAssets.SetActive(false);
        menuAssets.SetActive(true);
        Debug.Log("Opcoes fechadas");

        GameManager.Instance.gameTimer = timerSlider.value;
        GameManager.Instance.spawnRate = rateSlider.value;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);      
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.ClearScore();
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Jogo Fechado");
    }
    public void UpdateSliders()
    {
        timeText.text = timerSlider.value.ToString();
        rateText.text = rateSlider.value.ToString();
    }
}
