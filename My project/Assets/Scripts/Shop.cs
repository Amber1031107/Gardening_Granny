using TMPro.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using AK.Wwise; //Audio

public class Shop : MonoBehaviour, IInteractable
{
    [Header("Store Pages")]
    public GameObject storePrefab;
    public GameObject TrapsShop;
    public GameObject PlantsShop;
    public GameObject TreesShop;
    public GameObject PathwayShop;
    public GameObject MiscShop;
    public TextMeshProUGUI moneyText;
    [Header("Plant Store Buy Buttons")]
    public Button buyPlantbutton1;
    public Button buyPlantbutton2;
    public Button buyPlantbutton3;
    public Button buyPlantbutton4;
    public Button buyPlantbutton5;
    public Button buyPlantbutton6;
    [Header("Trap Store Buy Buttons")]
    public Button buyTrap1;
    [Header("Tree Store Buy Buttons")]
    public Button buyTreebutton1;
    public Button buyTreebutton2;
    public Button buyTreebutton3;
    public Button buyTreebutton4;
    public Button buyTreebutton5;
    [Header("Misc Store Buy Buttons")]
    public Button buyMiscbutton1;
    public Button buyMiscbutton2;
    public Button buyMiscbutton3;
    public Button buyMiscbutton4;
    public Button buyMiscbutton5;
    public Button buyMiscbutton6;
    [Header("Path Store Buy Buttons")]
    public Button buyPathbutton1;
    public Button buyPathbutton2;
    public Button buyPathbutton3;


    public PlayersInventory playersInventory;

    [Header("Audio")] //Audio
    public AK.Wwise.Event buyPlantEvent;
    public AK.Wwise.Event buyTrapEvent;
    public AK.Wwise.Event outOfMoneyEvent;
    public AK.Wwise.Event playComputerEvent;
    public AK.Wwise.Event closeShopEvent;
    public AK.Wwise.Event uiClickSound;

    public DayNightAudioController audioController; //AmbienceStop

    public static bool shopIsOpen = false; //Shop stopping world interactions


    public int moneyAmount;
    public int MoneyAmount
    {
        get { return moneyAmount; }
        set
        {
            moneyAmount = value;
            UpdateMoneyUI();
        }
    }

    void Start()
    {
        MoneyAmount = 1000;
        storePrefab.SetActive(false);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(false);
        PathwayShop.SetActive(false);
        TreesShop.SetActive(false);
        MiscShop.SetActive(false);
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "$" + moneyAmount.ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Shop.shopIsOpen) //Audio
            {
                closeShopEvent?.Post(gameObject);

                if (audioController != null)
                {
                    Debug.Log("audioController exists on close");
                    audioController.RestoreAmbienceAfterMenu();
                }
                else
                {
                    Debug.Log("audioController is NULL on close");
                }
            }

            storePrefab.SetActive(false);
            TrapsShop.SetActive(false);
            PlantsShop.SetActive(false);
            shopIsOpen = false; //Retriggers world interaction
        }
        buyPlantbutton1.interactable = moneyAmount >= 5;
        buyPlantbutton2.interactable = moneyAmount >= 10;
        buyPlantbutton3.interactable = moneyAmount >= 15;
        buyPlantbutton4.interactable = moneyAmount >= 20;
        buyPlantbutton5.interactable = moneyAmount >= 25;
        buyPlantbutton6.interactable = moneyAmount >= 30;
        buyTrap1.interactable = moneyAmount >= 5;
        buyMiscbutton1.interactable = moneyAmount >= 40;
        buyMiscbutton2.interactable = moneyAmount >= 60;
        buyMiscbutton3.interactable = moneyAmount >= 10;
        buyMiscbutton4.interactable = moneyAmount >= 20;
        buyMiscbutton5.interactable = moneyAmount >= 5;
        buyMiscbutton6.interactable = moneyAmount >= 15;
        buyTreebutton1.interactable = moneyAmount >= 5;
        buyTreebutton2.interactable = moneyAmount >= 10;
        buyTreebutton3.interactable = moneyAmount >= 15;
        buyTreebutton4.interactable = moneyAmount >= 20;
        buyTreebutton5.interactable = moneyAmount >= 25;
        buyPathbutton1.interactable = moneyAmount >= 5;
        buyPathbutton2.interactable = moneyAmount >= 5;
        buyPathbutton3.interactable = moneyAmount >= 5;

    }
    public void TrapsButton()
    {
        uiClickSound.Post(gameObject);
        TrapsShop.SetActive(true);
        PlantsShop.SetActive(false);
        TreesShop.SetActive(false);
        MiscShop.SetActive(false);
        PathwayShop.SetActive(false);
    }

    public void PlantsButton()
    {
        uiClickSound.Post(gameObject);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(true);
        TreesShop.SetActive(false);
        MiscShop.SetActive(false);
        PathwayShop.SetActive(false);
    }
    public void TreesButton()
    {
        uiClickSound.Post(gameObject);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(false);
        TreesShop.SetActive(true);
        MiscShop.SetActive(false);
        PathwayShop.SetActive(false);
    }
    public void PathwayButton()
    {
        uiClickSound.Post(gameObject);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(false);
        TreesShop.SetActive(false);
        MiscShop.SetActive(false);
        PathwayShop.SetActive(true);
    }
    public void MiscButton()
    {
        uiClickSound.Post(gameObject);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(true);
        TreesShop.SetActive(false);
        MiscShop.SetActive(true);
        PathwayShop.SetActive(false);
    }

    public void InteractLeftClick()
    {
        Debug.Log("SHOP OPEN FUNCTION RAN");

        playComputerEvent?.Post(gameObject);

        storePrefab.SetActive(true);
        Shop.shopIsOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (audioController != null)
        {
            Debug.Log("audioController exists");
            audioController.MuteAmbienceForMenu();
        }
        else
        {
            Debug.Log("audioController is NULL");
        }

    }
    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
        //moneyAmount = moneyAmount + 20;
        //UpdateMoneyUI();
    }

    void PlayPlantBuySound() //Audio
    {
        buyPlantEvent?.Post(gameObject);
    }

    void PlayTrapBuySound()
    {
        buyTrapEvent?.Post(gameObject);
    }

    void PlayErrorSound()
    {
        outOfMoneyEvent?.Post(gameObject);
    }

    public void BuyItem(ItemData item)
    {
        if (moneyAmount < item.cost)
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= item.cost;
        UpdateMoneyUI();
        playersInventory.AddItem(item.itemID);

        if (item.plantsOnGrass) PlayTrapBuySound();
        else PlayPlantBuySound();
    }
}
