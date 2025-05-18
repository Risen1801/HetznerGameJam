using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform scaleArm;
    [SerializeField] float rotationBorder = 70;
    [SerializeField] float savingTimer = 5;

    float losingTimer;
    bool player1Loosing;
    bool player2Loosing;

    void Update()
    {
        player1Loosing = scaleArm.eulerAngles.x > -rotationBorder;
        player2Loosing = scaleArm.eulerAngles.x < rotationBorder;

        if (player1Loosing || player2Loosing)
        {
            losingTimer = losingTimer + Time.deltaTime;
            if (losingTimer > savingTimer)
            {
                Time.timeScale = 0;
                // Check for winner;
            }
        }
        else
        {
            losingTimer = 0;
        }
    }
}
