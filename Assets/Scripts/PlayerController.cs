using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject abilityPanel;
    public Animator lightAnimator;

    public float currentHealth;
    public float maxHealth;
    public Image healthBar;
    public float hpRegen;

    [Header("Magic")]
    public float currentMagic;
    public float maxMagic;
    public Image magicBar;
    public float magicPrice;
    public float magicRegen;
    public int magicDamage;

    [Header("Level")]
    public float currentHPLV, currentHPRLV, currentMPLV, currentMPRLV;
    public TextMeshProUGUI currentHPText, currentHPRText, currentMPText, currentMPRText;

    public void SaveAbility()
    {
        SaveData.Save(maxHealth, "maxHealth");
        SaveData.Save(hpRegen, "hpRegen");
        SaveData.Save(maxMagic, "maxMagic");
        SaveData.Save(magicRegen, "magicRegen");
        SaveData.Save(magicDamage, "magicDamage");
        SaveData.Save(currentHPLV, "currentHPLV");
        SaveData.Save(currentHPRLV, "currentHPRLV");
        SaveData.Save(currentMPLV, "currentMPLV");
        SaveData.Save(currentMPRLV, "currentMPRLV");
    }

    public void LoadAbility()
    {
        maxHealth = SaveData.Load<float>("maxHealth");
        hpRegen = SaveData.Load<float>("hpRegen");
        maxMagic = SaveData.Load<float>("maxMagic");
        magicRegen = SaveData.Load<float>("magicRegen");
        magicDamage = SaveData.Load<int>("magicDamage");
        currentHPLV = SaveData.Load<float>("currentHPLV");
        currentHPRLV = SaveData.Load<float>("currentHPRLV");
        currentMPLV = SaveData.Load<float>("currentMPLV");
        currentMPRLV = SaveData.Load<float>("currentMPRLV");
    }

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadAbility();
        currentHealth = maxHealth;
        currentMagic = maxMagic;
        abilityPanel.SetActive(false);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        RegenMagic();
        RegenHealth();
    }

    public void UpdateUI()
    {
        currentHPText.text = "LV " + currentHPLV;
        currentHPRText.text = "LV " + currentHPRLV;
        currentMPText.text = "LV " + currentMPLV;
        currentMPRText.text = "LV " + currentMPRLV;

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = (float) currentHealth / (float) maxHealth;

        if (currentHealth < 0)
        {
            // Reset game
            Die();
        }
    }

    public void Die()
    {
        SaveAbility();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth;
        currentMagic = maxMagic;
    }

    public void DoMagic()
    {
        if (currentMagic >= magicPrice)
        {
            currentMagic -= magicPrice;
            magicBar.fillAmount = (float)currentMagic / (float)maxMagic;

            //Spell
            EnemyController.instance.currentEnemy.Damage(magicDamage);
            lightAnimator.SetTrigger("Active");
        }
    }

    public void RegenMagic()
    {

        currentMagic += magicRegen * (Time.deltaTime);
        if (currentMagic > maxMagic)
        {
            currentMagic = maxMagic;
        }
        magicBar.fillAmount = (float)currentMagic / (float)maxMagic;
    }

    public void RegenHealth()
    {

        currentHealth += hpRegen * (Time.deltaTime);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void AddHealth()
    {
        if (GameManager.instance.gold > 10000)
        {
            currentHealth += 100;
            maxHealth += 100;
            currentHPLV++;
            currentHPText.text = "LV " + currentHPLV;

            GameManager.instance.ReduceGold(10000);
        }
    }

    public void AddHealthRegen()
    {
        if (GameManager.instance.gold > 200000)
        {
            hpRegen += 0.1f;
            currentHPRLV++;
            currentHPRText.text = "LV " + currentHPRLV;

            GameManager.instance.ReduceGold(200000);
        }
    }
    public void AddMagicDamage()
    {
        if (GameManager.instance.gold > 100000)
        {
            magicDamage += 10;
            currentMPLV++;
            currentMPText.text = "LV " + currentMPLV;

            GameManager.instance.ReduceGold(100000);
        }
    }

    public void AddMagicRegen()
    {
        if (GameManager.instance.gold > 100000)
        {
            magicRegen += 0.1f;
            currentMPRLV++;
            currentMPRText.text = "LV " + currentMPRLV;

            GameManager.instance.ReduceGold(100000);
        }
    }

    public void OnOpenAbilityPanel()
    {
        abilityPanel.SetActive(true);
    }

    public void OnCloseAbilityPanel()
    {
        abilityPanel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        SaveAbility();
    }
}
