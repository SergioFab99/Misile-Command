using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyMissilePrefab;
    [SerializeField] private Transform[] spawnSlots;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int totalMissiles = 10;

    private float timer;
    private int spawnedCount = 0;
    private bool finishedSpawning = false;

    private void Update()
    {
        if (GameManager.I == null || GameManager.I.IsGameOver) return;

        if (!finishedSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnEnemy();
            }
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("EnemyMissile").Length == 0)
            {
                SceneManager.LoadScene("Victoria");
            }
        }
    }

    private void SpawnEnemy()
    {
        var activeCities = GameManager.I.GetActiveCities();
        if (activeCities == null || activeCities.Count == 0)
            return;

        var slot = spawnSlots[Random.Range(0, spawnSlots.Length)];
        var targetCity = activeCities[Random.Range(0, activeCities.Count)];

        var enemyObj = ObjectPool.I.Spawn(enemyMissilePrefab, slot.position, Quaternion.identity);

        if (enemyObj.TryGetComponent<EnemyMissile>(out var missile))
        {
            missile.Initialize(Vector3.zero, targetCity);
        }

        spawnedCount++;

        if (spawnedCount >= totalMissiles)
        {
            finishedSpawning = true;
        }
    }
}
