using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    [SerializeField] private InfosNiveaux levelInfo;

    [SerializeField] private GameObject objective;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private List<GameObject> enemies;

    public int repeatCount;

    public void StartNewWave()
    {
        SetRepeatCount();
        LaunchEnemy();
    }

    private void SetRepeatCount()
    {
        repeatCount = 5 + 2 * levelInfo.vague;
    }

    private void LaunchEnemy()
    {
        float cooldown = Random.Range(1f, 3f);
        Invoke("SpawnNewEnemy", cooldown);
        repeatCount--;
    }

    private void SpawnNewEnemy()
    {
        GameObject newEnemy = enemies[Random.Range(0, Mathf.Min(levelInfo.vague, enemies.Count))];

        newEnemy.transform.position = gameObject.transform.position;
        newEnemy.GetComponent<BasicEnemyAI>().objective = objective;

        Instantiate(newEnemy, enemyParent);

        if(repeatCount > 0) 
        {
            LaunchEnemy();
        }
    }
}
