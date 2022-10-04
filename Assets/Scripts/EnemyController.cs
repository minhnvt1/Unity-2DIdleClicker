using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public static EnemyController instance;
    public TextMeshProUGUI levelText;
    public Enemy currentEnemy;
    public GameObject[] enemies;
    public int currentIndex;
    public Transform canvas;

    public int enemyLevel;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        CreateEnemy();
        enemyLevel = 1;
    }

    public void CreateEnemy()
    {
        GameObject newEnemy = enemies[currentIndex];

        GameObject enemyObject = Instantiate(newEnemy, canvas);

        currentEnemy = enemyObject.GetComponent<Enemy>();

        currentIndex++;

        currentEnemy.goldToGive += enemyLevel;
        currentEnemy.maxHealth += enemyLevel * 100;
        currentEnemy.damage *= enemyLevel;

        levelText.text = "Wave " + enemyLevel + "/" + currentIndex;

        if (currentIndex == enemies.Length)
        {
            currentIndex = 0;
            enemyLevel++;
        }
    }

    public void DeadEnemy(GameObject enemy)
    {
        Destroy(enemy);
        CreateEnemy();
    }
}
