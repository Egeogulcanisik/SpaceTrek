using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // Birden fazla düşman türü ekleyebilirsin
    public float spawnRate = 2f;        // Kaç saniyede bir doğacak
    public float spawnDistance = 10f;   // Oyuncudan uzaklık
    private Transform player;
    public float minSpawnRate =0.5f;
    public float maxSpawnRate =3f;
    public float difficultyIncreaseRate=10f;
    public float nextDifficultyTime =0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnRate);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // Oyuncudan belli bir mesafede rastgele yön
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + randomDir * spawnDistance;

        // Rastgele düşman prefab seç
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
void update(){
if(Time.time > nextDifficultyTime){
spawnRate = Mathf.Max(minSpawnRate , spawnRate -0.2f);
nextDifficultyTime = Time.time + difficultyIncreaseRate;
}
 }
}