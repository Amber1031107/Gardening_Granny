using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    // ── Persistent-storage key ────────────────────────────────────────────────
    private const string PREF_KEY = "TutorialComplete";

    // ── Inspector references ──────────────────────────────────────────────────
    [Header("UI")]
    [Tooltip("The root Canvas that holds all tutorial UI. Will be toggled on/off.")]
    public Canvas tutorialCanvas;

    [Tooltip("Arrow image that rotates to point at the current target.")]
    public RectTransform arrowImage;

    [Tooltip("Optional hint label shown below the arrow.")]
    public TextMeshProUGUI hintText;

    [Header("World Targets")]
    [Tooltip("The 3-D computer the player must left-click to open the shop.")]
    public Transform computerTarget;

    [Tooltip("A dirt tile the player must plant something on (plantsOnDirt item).")]
    public Transform dirtTileTarget;

    [Tooltip("A grass tile the player must place a trap/grass-item on.")]
    public Transform grassTileTarget;

    [Tooltip("A PlacedPlant object the player must pick up with the shovel.")]
    public Transform placedPlantTarget;

    [Tooltip("A PlacedPlant object the player must right-click to rotate.")]
    public Transform rotatePlantTarget;

    [Header("Camera")]
    [Tooltip("Main camera used to convert world → screen positions for the arrow.")]
    public Camera playerCamera;

    [Header("Arrow Settings")]
    [Tooltip("Distance in pixels from the screen edge the arrow sits at when the target is off-screen.")]
    public float edgeMargin = 60f;

    [Tooltip("Speed at which the arrow smoothly follows its target.")]
    public float arrowLerpSpeed = 10f;

    // ── Private state ─────────────────────────────────────────────────────────
    private enum TutorialStep
    {
        ApproachComputer,       
        OpenShop,               
        BuyItem,                
        SwitchShopTab,          
        CloseShop,              
        PlantOnDirt,            
        PlantOnGrass,           
        PickUpItem,             
        RotateItem,             
        Complete                
    }

    private TutorialStep currentStep = TutorialStep.ApproachComputer;

    // Maps each step to the hint string shown to the player
    private static readonly Dictionary<TutorialStep, string> StepHints = new()
    {
        { TutorialStep.ApproachComputer,  "Walk to the computer and LEFT CLICK it to open the shop." },
        { TutorialStep.OpenShop,          "The shop is open! Buy an item." },
        { TutorialStep.BuyItem,           "Click a BUY button to purchase an item." },
        { TutorialStep.SwitchShopTab,     "Try switching to a different shop category." },
        { TutorialStep.CloseShop,         "Close the shop by pressing ESC." },
        { TutorialStep.PlantOnDirt,       "Select your item and LEFT CLICK a dirt patch to plant it." },
        { TutorialStep.PlantOnGrass,      "Now place a trap / grass item on unbroken ground." },
        { TutorialStep.PickUpItem,        "Equip the shovel and LEFT CLICK a placed item to pick it up." },
        { TutorialStep.RotateItem,        "RIGHT CLICK a placed item to rotate it." },
    };


    void Awake()
    {

        if (PlayerPrefs.GetInt(PREF_KEY, 0) == 1)
        {
            if (tutorialCanvas != null) tutorialCanvas.gameObject.SetActive(false);
            enabled = false;
            return;
        }
    }

    void Start()
    {
        if (!enabled) return;

        if (playerCamera == null)
            playerCamera = Camera.main;

        ApplyStep(currentStep);
    }

    void Update()
    {
        if (!enabled) return;
        if (currentStep == TutorialStep.Complete) return;

        UpdateArrow();
    }




    public void NotifyShopOpened()
    {
        if (currentStep == TutorialStep.ApproachComputer)
            AdvanceTo(TutorialStep.BuyItem);
    }


    public void NotifyItemBought()
    {
        if (currentStep == TutorialStep.BuyItem)
            AdvanceTo(TutorialStep.SwitchShopTab);
    }


    public void NotifyShopTabSwitched()
    {
        if (currentStep == TutorialStep.SwitchShopTab)
            AdvanceTo(TutorialStep.CloseShop);
    }


    public void NotifyShopClosed()
    {
        if (currentStep == TutorialStep.CloseShop)
            AdvanceTo(TutorialStep.PlantOnDirt);
    }


    public void NotifyPlantedOnDirt()
    {
        if (currentStep == TutorialStep.PlantOnDirt)
            AdvanceTo(TutorialStep.PlantOnGrass);
    }


    public void NotifyPlantedOnGrass()
    {
        if (currentStep == TutorialStep.PlantOnGrass)
            AdvanceTo(TutorialStep.PickUpItem);
    }


    public void NotifyItemPickedUp()
    {
        if (currentStep == TutorialStep.PickUpItem)
            AdvanceTo(TutorialStep.RotateItem);
    }


    public void NotifyItemRotated()
    {
        if (currentStep == TutorialStep.RotateItem)
            AdvanceTo(TutorialStep.Complete);
    }

    // ── Internal helpers ──────────────────────────────────────────────────────

    private void AdvanceTo(TutorialStep next)
    {
        currentStep = next;
        ApplyStep(currentStep);
    }

    private void ApplyStep(TutorialStep step)
    {
        if (step == TutorialStep.Complete)
        {
            CompleteTutorial();
            return;
        }

        // Update hint text
        if (hintText != null && StepHints.TryGetValue(step, out string hint))
            hintText.text = hint;

        Debug.Log($"[Tutorial] Step → {step}");
    }

    private void CompleteTutorial()
    {
        Debug.Log("[Tutorial] Tutorial complete — saving flag.");
        PlayerPrefs.SetInt(PREF_KEY, 1);
        PlayerPrefs.Save();

        if (tutorialCanvas != null)
            tutorialCanvas.gameObject.SetActive(false);

        enabled = false;
    }

    private Transform GetCurrentTarget()
    {
        return currentStep switch
        {
            TutorialStep.ApproachComputer => computerTarget,
            TutorialStep.OpenShop => computerTarget,
            TutorialStep.BuyItem => null,   // arrow hidden — UI is open
            TutorialStep.SwitchShopTab => null,
            TutorialStep.CloseShop => null,
            TutorialStep.PlantOnDirt => dirtTileTarget,
            TutorialStep.PlantOnGrass => grassTileTarget,
            TutorialStep.PickUpItem => placedPlantTarget,
            TutorialStep.RotateItem => rotatePlantTarget,
            _ => null
        };
    }

    private void UpdateArrow()
    {
        if (arrowImage == null) return;

        Transform target = GetCurrentTarget();

        // Hide the arrow when there's no world target (e.g. during open-shop steps)
        if (target == null)
        {
            arrowImage.gameObject.SetActive(false);
            return;
        }

        arrowImage.gameObject.SetActive(true);

        Vector3 worldPos = target.position;
        Vector3 screenPos = playerCamera.WorldToScreenPoint(worldPos);

        // Flip: target is behind the camera
        bool behindCamera = screenPos.z < 0f;
        if (behindCamera) screenPos *= -1f;

        float screenW = Screen.width;
        float screenH = Screen.height;
        Vector2 screenCenter = new Vector2(screenW * 0.5f, screenH * 0.5f);

        bool onScreen = !behindCamera
                        && screenPos.x >= 0 && screenPos.x <= screenW
                        && screenPos.y >= 0 && screenPos.y <= screenH;

        Vector2 targetAnchoredPos;

        if (onScreen)
        {
            // Convert screen position to Canvas (anchored) position
            // Canvas is Screen Space Overlay — screen coords == canvas coords
            targetAnchoredPos = new Vector2(screenPos.x - screenCenter.x,
                                            screenPos.y - screenCenter.y);
        }
        else
        {
            // Direction from screen center toward projected position
            Vector2 dir = new Vector2(screenPos.x - screenCenter.x,
                                      screenPos.y - screenCenter.y).normalized;

            // Clamp to screen rectangle minus edge margin
            float halfW = screenCenter.x - edgeMargin;
            float halfH = screenCenter.y - edgeMargin;

            // Find the intersection of the ray with the screen rect
            float tX = (dir.x != 0) ? halfW / Mathf.Abs(dir.x) : float.MaxValue;
            float tY = (dir.y != 0) ? halfH / Mathf.Abs(dir.y) : float.MaxValue;
            float t = Mathf.Min(tX, tY);

            targetAnchoredPos = dir * t;
        }

        // Smooth lerp toward the target position
        arrowImage.anchoredPosition = Vector2.Lerp(
            arrowImage.anchoredPosition,
            targetAnchoredPos,
            Time.deltaTime * arrowLerpSpeed);

        // Rotate arrow to point toward the target from screen center
        Vector2 toTarget = new Vector2(screenPos.x - screenCenter.x,
                                       screenPos.y - screenCenter.y);
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg - 90f;
        arrowImage.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
