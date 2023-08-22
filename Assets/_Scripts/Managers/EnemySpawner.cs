using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public ObjectPooler enemyPool;
        public float spawnChance;
    }

    public EnemySpawnInfo[] enemySpawnInfos; // Array of enemy spawn information
    public Transform[] spawnPoints;           // Array of spawn points

    public float initialSpawnDelay = 2f;
    public float spawnInterval = 5f;
    public float spawnIncreaseFactor = 0.98f;

    private float currentSpawnInterval;

    public void StartSpawning()
    {
        Reset();
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        Debug.Log("Stop");
    }

    private void Reset()
    {
        currentSpawnInterval = spawnInterval;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            float totalSpawnChance = 0f;
            foreach (var spawnInfo in enemySpawnInfos)
            {
                totalSpawnChance += spawnInfo.spawnChance;
            }

            float randomValue = Random.Range(0f, totalSpawnChance);

            GameObject selectedEnemyPrefab = null;
            foreach (var spawnInfo in enemySpawnInfos)
            {
                if (randomValue <= spawnInfo.spawnChance)
                {
                    selectedEnemyPrefab = spawnInfo.enemyPool.GetObjectFromPool();
                    break;
                }
                else
                {
                    randomValue -= spawnInfo.spawnChance;
                }
            }

            if (selectedEnemyPrefab != null)
            {
                int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

                // GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
                selectedEnemyPrefab.transform.SetPositionAndRotation(spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
                selectedEnemyPrefab.SetActive(true);
                // Customize enemy behavior or properties here if needed
            }

            yield return new WaitForSeconds(currentSpawnInterval);

            if(currentSpawnInterval > 0.5f)
            {
                currentSpawnInterval *= spawnIncreaseFactor;
            }
        }
    }
}
