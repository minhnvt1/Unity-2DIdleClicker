using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class ClickController : MonoBehaviour
{
    public bool button1;
    public bool button2;
    public bool button3;
    public bool button4;
    public bool button5;
    public bool button6;

    public List<float> autoClickerLastTime = new List<float>();
    public float autoClickerPrice;
    public float autoClickerPriceConverter;
    public float autoClickerTime = 1;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI autoClickerPriceText;
    public int damage;
    public float timer;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        timer += Time.deltaTime;

        for (int i = 0; i < autoClickerLastTime.Count; i++)
        {
            autoClickerLastTime[i] = timer;
            if (autoClickerLastTime[i] >= autoClickerTime)
            {
                EnemyController.instance.currentEnemy.Damage(damage);
                timer = 0;
            }
        }
    }

    public void OnBuyAutoClicker()
    {
        if (GameManager.instance.gold >= autoClickerPrice)
        {
            GameManager.instance.ReduceGold(autoClickerPrice);
            autoClickerLastTime.Add(timer);

            autoClickerPrice *= 3;

            if (autoClickerPrice < 1000)
            {
                autoClickerPriceText.text = autoClickerPrice.ToString();
            }
            if (autoClickerPrice >= 1000)
            {
                AutoClickerPriceConverter();
                autoClickerPriceText.text = autoClickerPrice.ToString("f2") + "K";
            }
            if (autoClickerPrice >= 1000000)
            {
                AutoClickerPriceConverter();
                autoClickerPriceText.text = autoClickerPrice.ToString("f2") + "M";
            }
            if (autoClickerPrice >= 1000000000)
            {
                AutoClickerPriceConverter();
                autoClickerPriceText.text = autoClickerPrice.ToString("f2") + "B";
            }
        }
    }

    public void AutoClickerPriceConverter()
    {
        if (autoClickerPrice > 1000) autoClickerPriceConverter = autoClickerPrice / 1000;
        if (autoClickerPrice > 1000000) autoClickerPriceConverter = autoClickerPrice / 1000000;
        if (autoClickerPrice > 1000000000) autoClickerPriceConverter = autoClickerPrice / 1000000000;
    }

    public void UpdateUI()
    {
        levelText.text = "LV " + autoClickerLastTime.Count.ToString();
        autoClickerPriceText.text = "" + autoClickerPrice.ToString();
    }

    public void Save()
    {
        if (button1 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers1");
            SaveData.Save(autoClickerPrice, "ClickersPrice1");
        }
        if (button2 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers2");
            SaveData.Save(autoClickerPrice, "ClickersPrice2");
        }
        if (button3 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers3");
            SaveData.Save(autoClickerPrice, "ClickersPrice3");
        }
        if (button4 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers4");
            SaveData.Save(autoClickerPrice, "ClickersPrice4");
        }
        if (button5 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers5");
            SaveData.Save(autoClickerPrice, "ClickersPrice5");
        }
        if (button6 == true)
        {
            SaveData.Save(autoClickerLastTime, "Clickers6");
            SaveData.Save(autoClickerPrice, "ClickersPrice6");
        }
    }

    public void Load()
    {
        if (button1 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers1");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice1");
        }
        if (button2 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers2");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice2");
        }
        if (button3 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers3");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice3");
        }
        if (button4 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers4");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice4");
        }
        if (button5 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers5");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice5");
        }
        if (button6 == true)
        {
            autoClickerLastTime = SaveData.Load<List<float>>("Clickers6");
            autoClickerPrice = SaveData.Load<float>("ClickersPrice6");
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
