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
        MoneyAmount = 1000;
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
       // if (moneyAmount > 30)
      //  {
//
      //  }
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
