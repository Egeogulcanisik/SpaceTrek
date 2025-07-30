using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // 3 prefab
    public float spawnRate = 5f;        // Kaç saniyede bir spawn olacak
    public float spawnMargin = 0.5f;    // Kenarlardan pay bırak

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating(nameof(SpawnPowerUp), 2f, spawnRate);
    }

    void SpawnPowerUp()
    {
        // Kamera sınırlarını hesapla
        Vector2 min = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = mainCam.ViewportToWorldPoint(new Vector2(1, 1));

        // Kenarlardan margin bırak
        float x = Random.Range(min.x + spawnMargin, max.x - spawnMargin);
        float y = Random.Range(min.y + spawnMargin, max.y - spawnMargin);

        Vector2 spawnPos = new Vector2(x, y);

        // Rastgele bir power-up seç
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);

        Instantiate(powerUpPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}

