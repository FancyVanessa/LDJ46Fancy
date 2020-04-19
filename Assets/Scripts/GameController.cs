using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public SpawnZone SpawnZones;
    public float SpawnPerSecond = 1.25f;
    float SpawnRate;
    float T = 0;

    public float EnemiesDead;
    public float WaveNumber = 1;
    public float EnemiesPerWave = 5;
    public float SpawnRateIncrease = 0.1f;
    float WaveProgress;
    float EnemiesSpawnedThisWave;
    float RoundTimer;

    HealthBar WaveBar;
    public TextMeshProUGUI WaveText;

    private void Start()
    {
        SpawnZones = FindObjectOfType<SpawnZone>();
        WaveBar = GetComponentInChildren<HealthBar>();
        WaveText.text = "Wave: " + WaveNumber.ToString();
    }

    void Update()
    {
        SpawnRate = 1 / SpawnPerSecond;
        T += Time.deltaTime;
        if(T > SpawnRate && EnemiesSpawnedThisWave < WaveNumber * EnemiesPerWave)
        {
            EnemiesSpawnedThisWave++;
            SpawnZones.SpawnEnemy();
            T = 0;
            RoundTimer = 0;
        }

        WaveProgress = EnemiesDead / (WaveNumber * EnemiesPerWave);
        WaveBar.SetSize(WaveProgress);
        if(WaveProgress == 1)
        {
            WaveText.text = "Wave Complete! New wave starting soon.";
            RoundTimer += Time.deltaTime;
            if(RoundTimer > 2.5f)
            {
                WaveNumber++;
                SpawnPerSecond += SpawnRateIncrease;
                EnemiesDead = 0;
                EnemiesSpawnedThisWave = 0;
                WaveText.text = "Wave: " + WaveNumber.ToString();
            }
        }
    }

    public void EnemyDeathCouter()
    {
        EnemiesDead++;
    }
}
