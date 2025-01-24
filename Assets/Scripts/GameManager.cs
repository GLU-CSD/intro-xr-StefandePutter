using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    private bool roundSpawner = false;
    private int score = 0;
    private float scoreTimer = 0f; // Timer om score te verhogen

    [SerializeField] public GameObject spawnpoint;
    [SerializeField] private List<GameObject> EnemySpawnpoints;
    public List<GameObject> enemies;

    // UI om de score te laten zien
    [SerializeField] private TextMeshProUGUI scoreText;

    // UI die getoond wordt bij game over
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject startRoundUI;
    [SerializeField] private GameObject roundWonUI;

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        if (gameActive)
        {
            //Time.deltaTime geeft aantal seconden sinds laatste Update
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= 1f) // Verhoog de score elke seconde
            {
                score++;
                scoreText.text = "Score: " + score;
                scoreTimer = 0f; // Reset de timer
            }

            if (score < 20f)
            {
                EnemySpawnpoints[1].SetActive(true);
            }

            if (score != 0 && score % 10 == 0)
            {
                endRound();
            }

            EnemyHealth enemy = FindFirstObjectByType<EnemyHealth>();
            if (!roundSpawner && enemy == null)
            {
                Debug.Log("Round ended");
                roundWonUI.SetActive(true);
                startRoundUI.SetActive(true);
                gameActive = false;
            }

        }
    }

    public void GameOver()
    {
        gameActive = false;

        // Vind alle vijanden in de scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Loop door alle gevonden vijanden en vernietig ze
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Doe hetzelfde met de spawners
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }

        // Toon de Game Over UI
        gameOverUI.SetActive(true);
    }

    private void endRound()
    {
        foreach (GameObject spawner in EnemySpawnpoints)
        {
            spawner.GetComponent<ObjectSpawner>().spawnInterval = Mathf.Infinity;
        }
        roundSpawner = false;
    }

    public void StartRound()
    {
        gameActive = true;
        roundSpawner = true;    
        startRoundUI.SetActive(false);

        foreach (GameObject spawner in EnemySpawnpoints)
        {
            spawner.GetComponent<ObjectSpawner>().spawnInterval = 0.5f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
