using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Points : MonoBehaviour, IInteractable
{

    public GameObject Kid;
    public Vector3 spawnPosition;
    public GameObject InvisableWall;
    public GameObject Fances;
    public Vector3 WallSpawnPosition;

    [Header("Score")]
    public int totalPoints = 0;

    [Tooltip("Optional — drag in a TextMeshPro UI text to display the score")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        Fances.SetActive(true);
        UpdateScoreUI();
    }

    public void InteractLeftClick()
    {
        // ── Count plants and sum their point values ───────────────────────────
        GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Flower");
        GameObject[] allTraps = GameObject.FindGameObjectsWithTag("Trap");

        totalPoints = 0;

        foreach (GameObject plant in allPlants)
        {
            PlantPoints pp = plant.GetComponent<PlantPoints>();
            totalPoints += pp != null ? pp.pointValue : 1; // default 1 if no component
        }

        foreach (GameObject trap in allTraps)
        {
            PlantPoints pp = trap.GetComponent<PlantPoints>();
            totalPoints += pp != null ? pp.pointValue : 1;
        }

        int placedCount = allPlants.Length + allTraps.Length;
        Debug.Log($"Plants: {allPlants.Length} | Traps: {allTraps.Length} | Total points: {totalPoints}");

        UpdateScoreUI();

        if (placedCount > 1)
        {
            Instantiate(Kid, spawnPosition, Quaternion.identity);
            Instantiate(InvisableWall, WallSpawnPosition, Quaternion.identity);
            Fances.SetActive(false);

            // Switch ambience to night
            FindObjectOfType<SeasonAmbienceController>().SetNight();
        }
    }

    public void InteractRightClick()
    {
        // throw new System.NotImplementedException();
    }

    // Call this from anywhere else that needs to add/remove points mid-round
    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log($"[Points] +{amount} → Total: {totalPoints}");
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + totalPoints.ToString();
    }


}
