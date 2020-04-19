using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector2 Center;
    public Vector2 Center2;
    public Vector3 Size;
    public Vector3 Size2;

    public GameObject EnemyPrefab;

    public void SpawnEnemy()
    {
        float ZoneChoice = Random.Range(0f, 1f);
        if (ZoneChoice >= 0.5f)
        {
            Vector2 SpawnPos = Center + new Vector2(Random.Range(-Size.x / 2, Size.x / 2), Random.Range(-Size.y / 2, Size.y / 2));
            Instantiate(EnemyPrefab, SpawnPos, Quaternion.identity);
        }
        else
        {
            Vector2 SpawnPos = Center2 + new Vector2(Random.Range(-Size2.x / 2, Size2.x / 2), Random.Range(-Size2.y / 2, Size2.y / 2));
            Instantiate(EnemyPrefab, SpawnPos, Quaternion.identity);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(Center, Size);
        Gizmos.DrawCube(Center2, Size2);
    }
}
