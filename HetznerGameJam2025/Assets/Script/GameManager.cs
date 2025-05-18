using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; set; }

    [SerializeField] float savingTimer = 5;

    float losingTimer;
    bool player1Loosing;
    bool player2Loosing;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (player1Loosing || player2Loosing)
        {
            losingTimer = losingTimer + Time.deltaTime;
            if (losingTimer > savingTimer)
            {
                Time.timeScale = 0;
                IsGameOver = true;
                // Check for winner;
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

}

public enum Player
{
    Player1,
    Player2
}
