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

    private int moneyAmount;
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
        MoneyAmount = 100;
        storePrefab.SetActive(false);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(false);
    }

    void UpdateMoneyUI()
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
        if (moneyAmount < 30)
        {
            buybutton6.interactable = false;
        }
        else
        {
            buybutton6.interactable = true;

        }
        if (moneyAmount < 25)
        {
            buybutton5.interactable = false;
        }
        else
        {
            buybutton5.interactable = true;

        }
        if (moneyAmount < 20)
        {
            buybutton4.interactable = false;
        }
        else
        {

            buybutton4.interactable = true;

        }
        if (moneyAmount < 15)
        {
            buybutton3.interactable = false;
        }
        else
        {

            buybutton3.interactable = true;

        }
        if (moneyAmount < 10)
        {
            buybutton2.interactable = false;
        }
        else
        {

            buybutton2.interactable = true;

        }
        if (moneyAmount < 5)
        {
            buybutton1.interactable = false;
        }
        else
        {

            buybutton1.interactable = true;

        }
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
        // throw new System.NotImplementedException();
        moneyAmount = moneyAmount + 20;
        UpdateMoneyUI();
    }

    public void BuyPlant1()
    {
        moneyAmount = moneyAmount - 5;
        UpdateMoneyUI();
        
        
    }
    public void BuyPlant2()
    {
        moneyAmount = moneyAmount - 10;
        UpdateMoneyUI();
        
        
    }
    public void BuyPlant3()
    {
        moneyAmount = moneyAmount - 15;
        UpdateMoneyUI();
        
        
    }
    public void BuyPlant4()
    {
        moneyAmount = moneyAmount - 20;
        UpdateMoneyUI();
        
        
    }
    public void BuyPlant5()
    {
        moneyAmount = moneyAmount - 25;
        UpdateMoneyUI();

        
        
    }
    public void BuyPlant6()
    {
        moneyAmount = moneyAmount - 30;
        UpdateMoneyUI();
        
        
    }
}
