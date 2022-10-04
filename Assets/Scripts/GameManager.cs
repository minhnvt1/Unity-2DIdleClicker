using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public float gold;
    public float goldConverter;

    public TextMeshProUGUI goldText;
    //public TextMeshProUGUI goldText;

    public Sprite[] background;
    private int currentBackground;
    public int currentEnemiesToBackground;
    public int maxEnemiesToBackground;
    public Image backgroundImage;

    public Animator goldAnimator;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gold = PlayerPrefs.GetFloat("Gold");
    }

    private void Update()
    {
        GoldConverter();
        GoldChange();
    }

    public void AddGold(float amount)
    {
        gold += amount;
        PlayerPrefs.SetFloat("Gold", gold);

        GoldConverter();
        GoldChange();
    }

    public void ReduceGold(float amount)
    {
        gold -= amount;
        PlayerPrefs.SetFloat("Gold", gold);

        GoldConverter();
        GoldChange();
    }

    public void BackgroundChecker()
    {
        currentEnemiesToBackground += 1;

        if(currentEnemiesToBackground == maxEnemiesToBackground)
        {
            currentEnemiesToBackground = 0;
            currentBackground++;
            if(currentBackground == background.Length)
            {
                currentBackground = 0;
            }
            backgroundImage.sprite = background[currentBackground];
        }
    }

    public void GoldChange()
    {
        if (gold < 1000)
        {
            goldText.text = gold.ToString();
            goldAnimator.SetBool("Idle", true);
            goldAnimator.SetBool("K", false);
            goldAnimator.SetBool("M", false);
            goldAnimator.SetBool("B", false);
        }
        if (gold >= 1000)
        {
            GoldConverter();
            goldText.text = goldConverter.ToString("F0") + "K";
            goldAnimator.SetBool("Idle", false);
            goldAnimator.SetBool("K", true);
            goldAnimator.SetBool("M", false);
            goldAnimator.SetBool("B", false);
        }
        if (gold >= 1000000)
        {
            GoldConverter();
            goldText.text = goldConverter.ToString("F1") + "M";
            goldAnimator.SetBool("Idle", false);
            goldAnimator.SetBool("K", false);
            goldAnimator.SetBool("M", true);
            goldAnimator.SetBool("B", false);
        }
        if (gold >= 1000000000)
        {
            GoldConverter();
            goldText.text = goldConverter.ToString("F1") + "B";
            goldAnimator.SetBool("Idle", false);
            goldAnimator.SetBool("K", false);
            goldAnimator.SetBool("M", false);
            goldAnimator.SetBool("B", true);
        }
    }

    public void GoldConverter()
    {
        if (gold < 1000) goldConverter = gold / 1;
        if (gold > 1000) goldConverter = gold / 1000;
        if (gold > 1000000) goldConverter = gold / 1000000;
        if (gold > 1000000000) goldConverter = gold / 1000000000;
    }
}
