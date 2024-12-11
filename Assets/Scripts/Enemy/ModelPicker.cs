using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPicker : MonoBehaviour
{
    [SerializeField] private List<GameObject> models;
    [SerializeField] private BasicEnemyAI enemyAI;
    [SerializeField] private float cooldownTime;
    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = cooldownTime;
        ChangeModel();
    }

    private void Update()
    {
        if(cooldown <= 0)
        {
            cooldown = cooldownTime;
            ChangeModel();
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void ChangeModel()
    {
        int id = Random.Range(0, models.Count);
        for(int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(i == id);
        }
    }
}
