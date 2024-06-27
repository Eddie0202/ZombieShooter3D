using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuHandler : MonoBehaviour
{
    public GameObject pauseCanvas;
    public bool isGamePaused;
    public GameObject gameOverCanvas;
    public bool isGameOver;
    public bool isWinGame;
    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject gameWinCanvas;
    public TextMeshProUGUI scoreWinnerText;
    // public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameOver){
            PauseGame();
        }
    }
    public void PauseGame(){
        if(!isGamePaused){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isGamePaused = true;
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }else{
            // player.SetActive(false);
            isGamePaused = false;
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void GameOver(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isGameOver = true;
        scoreText.text = "Score " + score;
        gameOverCanvas.SetActive(true);
    }
    public void WinGame(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isWinGame = true;
        scoreWinnerText.text = "Score " + score;
        gameWinCanvas.SetActive(true);
    }
    public void RestartGame(){
        string _roomName  = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        SceneManager.LoadScene(_roomName);
    }
    public void QuitGame(){
        SceneManager.LoadScene("Menu");
    }
}
