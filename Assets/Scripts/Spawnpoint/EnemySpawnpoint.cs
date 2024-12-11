using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    [SerializeField] private InfosNiveaux levelInfo;

    public GameObject objective;
    public Transform enemyParent;
    [SerializeField] private List<GameObject> enemies;

    public GestionnaireScore scoreManager;
    public GestionnaireVagues waveManager;

    public int repeatCount;
    public bool isActive;

    public void StartNewWave()
    {
        isActive = true;
        SetRepeatCount();
        LaunchEnemy();
    }

    private void SetRepeatCount()
    {
        repeatCount = 3 + 2 * levelInfo.vague;
    }

    private void LaunchEnemy()
    {
        float maxDelay = 3f;
        if(levelInfo.vague == 4) maxDelay = 2.5f;
        if(levelInfo.vague == 5) maxDelay = 2f;
        float cooldown = Random.Range(1f, maxDelay);
        Invoke("SpawnNewEnemy", cooldown);
        repeatCount--;
    }

    private void SpawnNewEnemy()
    {
        GameObject newEnemy = enemies[Random.Range(0, Mathf.Min(levelInfo.vague, enemies.Count))];

        newEnemy.transform.position = gameObject.transform.position;
        newEnemy.GetComponent<BasicEnemyAI>().objective = objective;
        newEnemy.GetComponent<BasicEnemyAI>().waveManager = waveManager;
        newEnemy.GetComponent<BasicEnemyAI>().scoreManager = scoreManager;

        Instantiate(newEnemy, enemyParent);

        if(repeatCount > 0) 
        {
            LaunchEnemy();
        }
        else
        {
            isActive = false;
        }
    }
}
