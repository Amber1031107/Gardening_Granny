using TMPro.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

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
        storePrefab.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
        //moneyAmount = moneyAmount + 20;
        //UpdateMoneyUI();
    }

    public void BuyPlant1()
    {
        moneyAmount -= 5;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant1);
    }

    public void BuyPlant2()
    {
        moneyAmount -= 10;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant2);
    }

    public void BuyPlant3()
    {
        moneyAmount -= 15;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant3);
    }

    public void BuyPlant4()
    {
        moneyAmount -= 20;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant4);
    }

    // ── Traps (buttons 5–6) ───────────────────────────────────────────────────

    public void BuyPlant5()
    {
        moneyAmount -= 25;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant5);
    }

    public void BuyPlant6()
    {
        moneyAmount -= 30;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.FlowerSpringPlant6);
    }

    public void BuyTrap1()
    {
        moneyAmount -= 5;
        UpdateMoneyUI();
        playersInventory.AddItem(itemType.Trap);
    }
}
