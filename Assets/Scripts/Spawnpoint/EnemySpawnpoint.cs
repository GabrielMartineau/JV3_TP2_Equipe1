using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    // Variable temporaire: a remplacer lorsqu'on aura un systeme de vague
    [SerializeField] private int waveCount;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform enemyParent;

    [SerializeField] private GameObject objective;

    private int repeatCount;
    // Start is called before the first frame update
    
    private void Start()
    {
        // Ligne temporaire: LaunchEnemies va être appelée par une autre fonction.
        SetRepeatCount();
        LaunchEnemy();
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void SetRepeatCount()
    {
        repeatCount = 5 + 2 * waveCount;
    }

    public void LaunchEnemy()
    {

        float cooldown = Random.Range(1f, 3f);
        Invoke("SpawnNewEnemy", cooldown);
        repeatCount--;
        
        
        
    }

    private void SpawnNewEnemy()
    {
        GameObject newEnemy = enemies[Random.Range(0, waveCount)];

        newEnemy.transform.position = gameObject.transform.position;
        newEnemy.GetComponent<BasicEnemyAI>().objective = objective;

        Instantiate(newEnemy, enemyParent);

        if(repeatCount > 0) 
        {
            LaunchEnemy();
        }
    }
}
