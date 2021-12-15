using System.Collections;
using UnityEngine;

public class WaveSpawneR : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    int nextWave = 0;

    public int NextWave 
    {
        get { return nextWave + 1; } 
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    float waveCountdown;
    public float WaveCountdown 
    { 
        get { return waveCountdown; } 
    }

    float searchCountdown = 1f;

    SpawnState state = SpawnState.COUNTING;

    public SpawnState State 
    {
        get { return state; } 
    }

    void Start()
    {
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points referenced.");

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (State == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (WaveCountdown <= 0)
        {
            if (State != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[NextWave]));
            }            
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (NextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        Debug.Log($"Spawning Wave: {_wave.name}");
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // Spawn enemy
        Debug.Log($"Spawning Enemy: {_enemy.name}");

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
