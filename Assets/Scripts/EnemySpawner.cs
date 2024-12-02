using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   public static EnemySpawner Instance;

   [System.Serializable]
   public class WaveSettings
   {
      public List<EnemySO> enemyTypes;
      public float spawnInterval;
      public int enemiesPerWave;
      public float waveDuration;
   }
   
   [SerializeField] private List<WaveSettings> waves;
   [SerializeField] private List<Collider> spawnAreas;
   [SerializeField] private Transform tower;

   private Dictionary<EnemySO, Queue<GameObject>> enemyPools = new Dictionary<EnemySO, Queue<GameObject>>();
   private int currentWaveIndex = 0;
   private float waveTimer = 0f;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      StartCoroutine(SpawnLoop());
   }

   private IEnumerator SpawnLoop()
   {
      while (currentWaveIndex < waves.Count)
      {
         WaveSettings currentWave = waves[currentWaveIndex];
         waveTimer = currentWave.waveDuration;

         while (waveTimer > 0)
         {
            SpawnEnemies(currentWave);
            yield return new WaitForSeconds(currentWave.spawnInterval);
            waveTimer -= currentWave.spawnInterval;
         }

         currentWaveIndex++;
      }
   }

   private void SpawnEnemies(WaveSettings wave)
   {
      for (int i = 0; i < wave.enemiesPerWave; i++)
      {
         EnemySO enemyType = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Count)];
         GameObject enemy = GetEnemyFromPool(enemyType);
         Vector3 spawnPosition = GetRandomSpawnPosition();
         enemy.transform.position = spawnPosition;
         enemy.transform.rotation = Quaternion.LookRotation(tower.position - spawnPosition);
         enemy.SetActive(true);
         enemy.GetComponent<EnemyController>().Initialize(enemyType,tower);
      }
   }

   private GameObject GetEnemyFromPool(EnemySO enemyType)
   {
      if (!enemyPools.ContainsKey(enemyType))
      {
         enemyPools[enemyType] = new Queue<GameObject>();
      }

      if (enemyPools[enemyType].Count > 0)
      {
        return enemyPools[enemyType].Dequeue();
      }
      else
      {
         GameObject newEnemy = Instantiate(enemyType.enemyPrefab);
         newEnemy.SetActive(false);
         return newEnemy;
      }
   }

   private Vector3 GetRandomSpawnPosition()
   {
      Collider spawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
      Bounds bounds = spawnArea.bounds;
      Vector3 randomPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y, Random.Range(bounds.min.z, bounds.max.z));
      return randomPosition;
   }

   public void ReturnEnemyToPool(EnemySO enemyType, GameObject enemy)
   {
      enemy.SetActive(false);
      enemyPools[enemyType].Enqueue(enemy);
   }
}
