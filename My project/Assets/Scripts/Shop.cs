using TMPro.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
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
    }

    public void ShowShop()
    {
        storePrefab.SetActive(true);
        TrapsShop.SetActive(false);
        PlantsShop.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
}
