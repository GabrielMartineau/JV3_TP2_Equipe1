using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerPointGenerator : MonoBehaviour
{
      public static SpawnerPointGenerator Instance  {get; private set;}  
      [SerializeField] private int numSpawnerPoints = 5;
      [SerializeField] private float minDistance = 2f;
      [SerializeField] private float maxDistance = 3f;
      [SerializeField] private float maxHeight = 0f;
        [SerializeField] private GameObject spawner;
        [SerializeField] private GameObject objectif;
        [SerializeField] private Transform transformWaveManager;
        public GestionnaireScore scoreManager;
        public GestionnaireVagues waveManager;



private void Awake()
  {
    if(Instance != null && Instance != this)    {
       Destroy(gameObject);
    }
    else {
        Instance = this;
    }
  }

  public void GenerateSpawnerPoints(Vector3 startPosition)
    {
        for (int i = 0; i < numSpawnerPoints; i++)
            {
        GameObject spawnerPoint = Instantiate(spawner, transformWaveManager);
        spawnerPoint.name = "SpawnerPoint" + i;
        spawnerPoint.transform.position = GetRandomPosition(startPosition);
        spawnerPoint.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        spawnerPoint.GetComponent<EnemySpawnpoint>().objective = objectif;
        spawnerPoint.GetComponent<EnemySpawnpoint>().enemyParent = gameObject.transform;
        spawnerPoint.GetComponent<EnemySpawnpoint>().waveManager = waveManager;
        spawnerPoint.GetComponent<EnemySpawnpoint>().scoreManager = scoreManager;
    }
    }


public Vector3 GetRandomPosition(Vector3 startPosition)
 {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0,360) * Mathf.Deg2Rad;
        float x = startPosition.x + distance * Mathf.Cos(angle);
        float y = startPosition.y + Random.Range(0,maxHeight);
        float z = startPosition.z + distance * Mathf.Sin(angle);
        return new Vector3(x,y,z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
