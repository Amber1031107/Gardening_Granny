using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using AK.Wwise;

public class Points : MonoBehaviour, IInteractable
{

    public GameObject Kid;
    public Vector3 spawnPosition;
    public GameObject InvisableWall;
    public GameObject Fances;
    public Vector3 WallSpawnPosition;

    public GameObject pointsPrefab;

    [Header("PointsScoreBoardScreen")]
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
        UpdateScoreUI();
    }

    public void InteractLeftClick()
    {
        cutsceneButtonEvent?.Post(gameObject); //Button Press Audio

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
           
            //set sun to night
            sun.intensity = 0.05f;
            sun.color = new Color(0.4f, 0.5f, 0.8f); // cool blue
            sun.transform.rotation = Quaternion.Euler(10f, 170f, 0f);
            RenderSettings.ambientLight = new Color(0.02f, 0.02f, 0.05f);

            // --- AMBIENCE ---
            var dayNightController = Object.FindFirstObjectByType<DayNightAudioController>();
            if (dayNightController != null)
            {
                dayNightController.SetNight();
            }
            else
            {
                Debug.LogWarning("DayNightAudioController not found in scene!");
            }

            // --- MUSIC ---
            var musicController = Object.FindFirstObjectByType<MusicAudioController>();
            if (musicController != null)
            {
                musicController.PlayNight();  // Starts night music from beginning
            }
            else
            {
                Debug.LogWarning("MusicAudioController not found in scene!");
            }
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
