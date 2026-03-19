using TMPro.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using AK.Wwise; //Audio

public class Shop : MonoBehaviour, IInteractable
{
    public GameObject storePrefab;
    public GameObject TrapsShop;
    public GameObject PlantsShop;
    public TextMeshProUGUI moneyText;

    public Button buybutton1;
    public Button buybutton2;
    public Button buybutton3;
    public Button buybutton4;
    public Button buybutton5;
    public Button buybutton6;
    public Button buyTrap1;

    public PlayersInventory playersInventory;

    [Header("Audio")] //Audio
    public AK.Wwise.Event buyPlantEvent;
    public AK.Wwise.Event buyTrapEvent;
    public AK.Wwise.Event outOfMoneyEvent;
    public AK.Wwise.Event playComputerEvent;

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
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "$" + moneyAmount.ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            storePrefab.SetActive(false);
            TrapsShop.SetActive(false);
            PlantsShop.SetActive(false);
            shopIsOpen = false; //Retriggers world interaction
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        buybutton1.interactable = moneyAmount >= 5;
        buybutton2.interactable = moneyAmount >= 10;
        buybutton3.interactable = moneyAmount >= 15;
        buybutton4.interactable = moneyAmount >= 20;
        buybutton5.interactable = moneyAmount >= 25;
        buybutton6.interactable = moneyAmount >= 30;
        buyTrap1.interactable = moneyAmount >= 5;

    }
    public void TrapsButton()
    {

        TrapsShop.SetActive(true);
        PlantsShop.SetActive(false);
    }

    public void PlantsButton()
    {

        TrapsShop.SetActive(false);
        PlantsShop.SetActive(true);
    }

    public void InteractLeftClick()
    {
        playComputerEvent?.Post(gameObject); //Audio

        storePrefab.SetActive(true);
        Shop.shopIsOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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


    public void BuyPlant1()
    {

        if (moneyAmount < 5) //Audio
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 5;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant1);
        PlayPlantBuySound(); //Audio
    }

    public void BuyPlant2()
    {

        if (moneyAmount < 10) //Audio
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 10;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant2);
        PlayPlantBuySound(); //Audio
    }

    public void BuyPlant3()
    {

        if (moneyAmount < 15)
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 15;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant3);
        PlayPlantBuySound(); //Audio
    }

    public void BuyPlant4()
    {

        if (moneyAmount < 20) //Audio
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 20;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant4);
        PlayPlantBuySound(); //Audio
    }

    // ── Traps (buttons 5–6) ───────────────────────────────────────────────────

    public void BuyPlant5()
    {

        if (moneyAmount < 25) //Audio
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 25;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant5);
        PlayPlantBuySound(); //Audio
    }

    public void BuyPlant6()
    {

        if (moneyAmount < 30) //Audio
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 30;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant6);
        PlayPlantBuySound(); //Audio
    }

    public void BuyTrap1()
    {

        if (moneyAmount < 5)
        {
            PlayErrorSound();
            return;
        }

        moneyAmount -= 5;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.Trap);
        PlayTrapBuySound(); //Audio
    }
}
