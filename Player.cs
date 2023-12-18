using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour,IDamageable
{

    [SerializeField] private int Health = 300;

    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI roundDisplay;
    public int rounds = 0;
    public Slider slider;

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        slider.value = Health;
        if (Health <= 0)
        {

            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
