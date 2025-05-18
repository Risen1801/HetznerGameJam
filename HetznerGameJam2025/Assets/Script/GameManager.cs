using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; set; }

    [SerializeField] float savingTimer = 5;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] GameObject overlay;

    float losingTimer;
    bool player1Loosing;
    bool player2Loosing;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            overlay.SetActive(true);
            text.text = "Pause";
            buttonText.text = "Continue";
        }
        if (player1Loosing || player2Loosing)
        {
            losingTimer = losingTimer + Time.deltaTime;
            if (losingTimer > savingTimer)
            {
                Time.timeScale = 0;
                IsGameOver = true;
                overlay.SetActive(true);
                buttonText.text = "New game";
                if(player1Loosing)
                { 
                    text.text = "Player 2 wins!";
                }
                if (player2Loosing)
                {
                    text.text = "Player 1 wins!";
                }
            }
        }
        else
        {
            losingTimer = 0;
        }
    }

    public void DeathImminent(Player player)
    {
        if (player == Player.Player1)
        {
            player1Loosing = true;
        }
        if (player == Player.Player2)
        {
            player2Loosing = true;
        }
    }

    public void DeathAverted()
    {
        player1Loosing = false;
        player2Loosing = false;
    }

    public void ResumeGame()
    {
        if(IsGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            overlay.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

public enum Player
{
    Player1,
    Player2
}
