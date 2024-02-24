
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class GameManager : MonoBehaviour
{
    public Tower tower;
    public List<Enemy> enemies;
    public Transform[] spawnPoints;

    // Difficulty variables
    public float baseSpawnRate = 1.0f;
    public float spawnRate;
    public float spawnRateIncrease = 0.1f; // How much spawn rate increases per wave
    public int currentWave = 1;
    public int maxWaves = 10; // Adjust based on your desired game length

    // Game state variables
    public enum GameState { Start, Running, WaveEnd, GameOver }
    public GameState currentState = GameState.Start;
    public GameObject[] enemyPrefabs;

     public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    Debug.LogError("GameManager instance not found!");
                }
            }
            return instance;
        }
    }

    void Start()
    {
        // Add spawn points to array if not assigned manually
        if (spawnPoints.Length == 0)
        {
            spawnPoints = GetComponentsInChildren<Transform>(true);
            spawnPoints = spawnPoints.Where(t => t.tag == "SpawnPoint").ToArray();
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Start:
                // Handle start of the game, display instructions, start first wave
                StartWave();
                currentState = GameState.Running;
                break;

            case GameState.Running:
                UpdateEnemies();
                SpawnEnemy();
                CheckWinLose();
                break;

            case GameState.WaveEnd:
                // Handle end of wave, display wave completion message, start next wave
                // with increased difficulty
                currentWave++;
                if (currentWave > maxWaves)
                {
                    GameOver();
               }
               else
                {
                    StartWave();
                    currentState = GameState.Running;
                }
                break;

           case GameState.GameOver:
                // Handle game over state, display message, offer restart or quit options
                break;
        }
    }

    void StartWave()
    {
        // Reset wave-specific variables
        enemies.Clear();
        // Adjust spawn rate based on current wave
        spawnRate = baseSpawnRate + (currentWave - 1) * spawnRateIncrease;
    }

    void UpdateEnemies()
    {
        // Move and update active enemies
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].isDead)
            {
                Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
            }
            else
            {
                enemies[i].Update();
            }
        }
    }

    void SpawnEnemy()
    {
        if (  //Time.time % spawnRate == 0 &&
         enemies.Count < currentWave * 3) // Adjust to control enemy density
       {
            // Choose random spawn point and enemy type
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            int enemyType = Random.Range(0,enemyPrefabs.Length ); // Number of enemy types);

            // Instantiate enemy prefab and add it to the enemies list
            Enemy enemy = Instantiate(enemyPrefabs[enemyType], spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();;
            enemy.SetTarget(tower.transform); // Set enemy target
           enemies.Add(enemy);
        }
    }

    void CheckWinLose()
    {
        if (enemies.Count == 0 && isWaveCleared())
        {
            currentState = GameState.WaveEnd;
        }
        else if (tower.health <= 0)
        {
            GameOver();
        }
    }

    bool isWaveCleared()
    {
        // Check if all enemies have been spawned and reached the tower or died
        foreach (Transform spawnPoint in spawnPoints)
        {
           bool isTowerCollided = Physics.OverlapSphere(spawnPoint.position, 1.0f).FirstOrDefault(col => col.CompareTag("Enemy")) != null;

            if (isTowerCollided)

           

            {
                return false;
            }
        }
        return true;
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        // Handle game over actions, display message, etc.
    }
}
