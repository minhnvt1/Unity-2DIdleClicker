using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public float goldToGive;
    public Image healthBar;
    public int damage;
    public int playerDamage;

    public void Damage(int amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.AddGold(goldToGive);
        EnemyController.instance.DeadEnemy(gameObject);
        GameManager.instance.BackgroundChecker();
    }

    public void DoDamage()
    {
        PlayerController.instance.TakeDamage(damage);
    }
}
