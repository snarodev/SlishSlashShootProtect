using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;

    public Image healthImage;

    int health;

    void Start()
    {
        health = maxHealth;
    }

    public void Damage(int amount, Vector3 damageDirection)
    {
        health -= amount;

        healthImage.fillAmount = (float)health / (float)maxHealth;

        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>().GameOver();
        }
    }
}
