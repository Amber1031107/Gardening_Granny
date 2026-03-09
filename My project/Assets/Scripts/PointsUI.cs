using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public GameObject scorePanel;           // Panel that holds the score
    public TextMeshProUGUI scoreText;


    void Start()
    {
        // Hide the panel at the start
        scorePanel.SetActive(false);

    }

    // Called by EnemyAI when trap is hit
    public void ShowScore(int points)
    {
        scoreText.text = "Points: " + points;
        scorePanel.SetActive(true);

    }
}
