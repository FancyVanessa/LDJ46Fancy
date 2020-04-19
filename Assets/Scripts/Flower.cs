using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    public float Health;
    public float MaxHealth = 100f;
    float HealthPercent;
    HealthBar bar;
    PauseMenu pauseMenu;
    bool GameOver = false;
    public AudioClip FlowerHurt;
    public AudioClip FlowerDeath;
    AudioSource audioSource;

    void Start()
    {
        bar = GetComponentInChildren<HealthBar>();
        Health = MaxHealth;
        pauseMenu = FindObjectOfType<PauseMenu>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float Damage)
    {
        if (Health >= 0 )
        {
            audioSource.PlayOneShot(FlowerHurt);
        }
        Health -= Damage;
        HealthPercent = Health / MaxHealth;
        bar.SetSize(HealthPercent);
        if (Health <= 0 && !GameOver)
        {
            audioSource.PlayOneShot(FlowerDeath);
            GameOver = true;
            pauseMenu.GameOver();
        }
    }
}
