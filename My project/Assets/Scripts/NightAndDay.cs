using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NightAndDay : MonoBehaviour
{
    public Light sun;

    public GameObject PointsPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsPrefab.SetActive(false);
        sun.intensity = 1f;
        sun.color = Color.white;
        sun.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.2f);

        // --- AMBIENCE ---
        var ambience = Object.FindFirstObjectByType<DayNightAudioController>();
        ambience?.SetDay();

        // --- MUSIC ---
        var music = Object.FindFirstObjectByType<MusicAudioController>();
        music?.PlayDay();  // Start Day music at track start
    }

    public void nextDay()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("InvisableWall");
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }

        PointsPrefab.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sun.intensity = 1f;
        sun.color = Color.white;
        sun.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.2f);

        // --- AMBIENCE ---
        var ambience = Object.FindFirstObjectByType<DayNightAudioController>();
        ambience?.SetDay();

        // --- MUSIC ---
        var music = Object.FindFirstObjectByType<MusicAudioController>();
        if (music != null)
        {
            music.StopMusic();   // stop night music
            music.PlayDay();     // start day music from beginning
        }
    }
}
