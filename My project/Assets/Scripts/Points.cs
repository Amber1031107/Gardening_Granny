using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using AK.Wwise;

public class Points : MonoBehaviour, IInteractable
{

    public bool kidSpawned = false;

    [Header("Spawn Area")]
    public Vector3 spawnAreaCenter;   // Centre of the random spawn zone
    public Vector3 spawnAreaSize;     // Width/Height/Depth of the zone (like a box)



    //public GameObject GameEndScreen;

    [Header("Kid spawn stuff")]
    public GameObject Kid;
    public Vector3 spawnPosition;
    public GameObject InvisableWall;
    public GameObject Fances;
    public Vector3 WallSpawnPosition;

    public GameObject pointsPrefab;

    [Header("PointsScoreBoardScreen")]
    public GameObject scoreBoardPanel;
    public TextMeshProUGUI moneyText;
    public int incomeAmount;
    public TextMeshProUGUI DaysLeft;
    public int DaysLeftAmount;
    public TextMeshProUGUI Rating;
    public int RatingAmount;
    public Light sun;

    [Header("Audio")]
    public AK.Wwise.Event cutsceneButtonEvent; //Audio

    [Header("Score")]
    public int totalPoints = 0;

    public Shop shopscript;

    [Tooltip("Optional — drag in a TextMeshPro UI text to display the score")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        Fances.SetActive(true);
        DaysLeftAmount = 3;

        if (scoreBoardPanel != null)
            scoreBoardPanel.SetActive(false);
        //GameEndScreen.SetActive(false);
        UpdateScoreUI();
    }

    public void InteractLeftClick()
    {

        cutsceneButtonEvent?.Post(gameObject);

        GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Flower");
        GameObject[] allTraps = GameObject.FindGameObjectsWithTag("Trap");

        totalPoints = 0;

        foreach (GameObject plant in allPlants)
        {
            PlantPoints pp = plant.GetComponent<PlantPoints>();
            totalPoints += pp != null ? pp.pointValue : 1;
        }

        foreach (GameObject trap in allTraps)
        {
            PlantPoints pp = trap.GetComponent<PlantPoints>();
            totalPoints += pp != null ? pp.pointValue : 1;
        }

        int placedCount = allPlants.Length + allTraps.Length;
        Debug.Log($"Plants: {allPlants.Length} | Traps: {allTraps.Length} | Total points: {totalPoints}");

        UpdateScoreUI();

        if (placedCount > 1 && !kidSpawned)  // ← Only runs if kid hasn't spawned yet
        {
            kidSpawned = true;  // ← Lock it immediately so it can never run again

            // Random position within the defined box area
            Vector3 randomSpawn = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f),
                spawnAreaCenter.y,
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2f, spawnAreaCenter.z + spawnAreaSize.z / 2f)
            );

            Instantiate(Kid, randomSpawn, Quaternion.identity);
            Instantiate(InvisableWall, WallSpawnPosition, Quaternion.identity);
            Fances.SetActive(false);

            sun.intensity = 0.05f;
            sun.color = new Color(0.4f, 0.5f, 0.8f);
            sun.transform.rotation = Quaternion.Euler(10f, 170f, 0f);
            RenderSettings.ambientLight = new Color(0.02f, 0.02f, 0.05f);

            var dayNightController = Object.FindFirstObjectByType<DayNightAudioController>();
            if (dayNightController != null) dayNightController.SetNight();
            else Debug.LogWarning("DayNightAudioController not found in scene!");

            var musicController = Object.FindFirstObjectByType<MusicAudioController>();
            if (musicController != null) musicController.PlayNight();
            else Debug.LogWarning("MusicAudioController not found in scene!");
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

        if (shopscript == null)
        {
            Debug.LogError("[Points] shopscript is not assigned!");
            return;
        }

        Fances.SetActive(true);
        UpdateScoreUI();

        // Show the scoreboard panel
        if (scoreBoardPanel != null)
            scoreBoardPanel.SetActive(true);
        else
            Debug.LogWarning("[Points] scoreBoardPanel is not assigned!");
    }

    private void UpdateScoreUI()
    {
        shopscript.moneyAmount += 500;
        DaysLeftAmount -= 1;
        if (scoreText != null)
            scoreText.text = "Score: " + totalPoints.ToString();

        // Pull money directly from shop so both UIs always match
        if (moneyText != null && shopscript != null)
            moneyText.text = "$" + shopscript.moneyAmount.ToString();

        if (DaysLeft != null)
            DaysLeft.text = DaysLeftAmount.ToString();

        if (DaysLeftAmount < 1)
        {
            // GameEndScreen.SetActive(true);
        }

        shopscript.UpdateMoneyUI();

        if (Rating != null)
        {
            // Fixed: no gaps between thresholds, use >= and <=
            if (totalPoints >= 40)
                Rating.text = "S";
            else if (totalPoints >= 35)
                Rating.text = "A";
            else if (totalPoints >= 30)
                Rating.text = "B";
            else if (totalPoints >= 25)
                Rating.text = "C";
            else if (totalPoints >= 20)
                Rating.text = "D";
            else if (totalPoints >= 15)
                Rating.text = "E";
            else if (totalPoints >= 10)
                Rating.text = "F";
            else
                Rating.text = "F"; // catch-all so it's never blank
        }
    }
}
