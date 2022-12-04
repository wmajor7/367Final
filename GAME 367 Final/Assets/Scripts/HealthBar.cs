using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI Health;
    public CharacterController my_Char;
    public GameObject Player;

    private void Start()
    {
        my_Char = Player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        Health.text = my_Char.health.ToString() + "/" + my_Char.maxHealth.ToString(); ;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
