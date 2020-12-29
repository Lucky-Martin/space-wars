using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waves;
    [SerializeField] bool loopWaves = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWaves());
        }
        while (loopWaves);
    }

    private IEnumerator SpawnWaves()
    {
        for(int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            WaveConfig currentWave = waves[waveIndex];
            yield return StartCoroutine(SpawnEnemies(currentWave));
        }
    }

    private IEnumerator SpawnEnemies(WaveConfig wave)
    {
        for(int enemyCount = 0; enemyCount < wave.enemiesCount; enemyCount++)
        {
            var enemy = Instantiate(wave.enemyPrefab, wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
