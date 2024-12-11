using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    public void OnDeath()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            SpawnNewEnemy(enemies[i]);
        }
    }

    private void SpawnNewEnemy(GameObject newEnemy)
    {
        GameObject trojan = gameObject;

        newEnemy.transform.position = trojan.transform.position;
        newEnemy.GetComponent<BasicEnemyAI>().objective = trojan.GetComponent<BasicEnemyAI>().objective;
        newEnemy.GetComponent<BasicEnemyAI>().waveManager = trojan.GetComponent<BasicEnemyAI>().waveManager;
        newEnemy.GetComponent<BasicEnemyAI>().scoreManager = trojan.GetComponent<BasicEnemyAI>().scoreManager;

        Instantiate(newEnemy, trojan.transform.parent);
    }
}
