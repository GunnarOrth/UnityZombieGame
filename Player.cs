using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour,IDamageable
{

    [SerializeField] private int Health = 100;
    public float healthRegen = 10.0f;
    public float currentHealth = 0;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI finalRoundDisplay;
    public TextMeshProUGUI enemiesKilledDisplay;
    public Slider slider;
    public float timeSinceHit;
    public Spawner textStuff;

    public void Awake()
    {
        gameOver.gameObject.SetActive(false);
        finalRoundDisplay.gameObject.SetActive(false);
        enemiesKilledDisplay.gameObject.SetActive(false);
        currentHealth = Health;
    }


    private void Update()
    {
        timeSinceHit += Time.deltaTime;

        //print("walta");
        if(currentHealth <= Health -.01 && timeSinceHit > 4)
        {
            
            currentHealth += healthRegen * Time.deltaTime;

            if(currentHealth >= Health)
            {
                currentHealth = Health;
            }
        }
        slider.value = currentHealth;
    }

    public void TakeDamage(int Damage)
    {
        timeSinceHit = 0;
        currentHealth -= Damage;
        slider.value = currentHealth;
        if (currentHealth <= 0)
        {
            gameOver.gameObject.SetActive(true);
            gameObject.SetActive(false);

            finalRoundDisplay.gameObject.SetActive(true);
            finalRoundDisplay.SetText("ROUNDS SURVIVED: " + textStuff.Round);
            enemiesKilledDisplay.gameObject.SetActive(true);
            enemiesKilledDisplay.SetText("ENEMIES KILLED: " + textStuff.totalEnemiesKilled);
        }
    }

    
    public Transform GetTransform()
    {
        return transform;
    }
}
