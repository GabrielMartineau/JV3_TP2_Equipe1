using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject objective;
    [SerializeField] private EnemyType enemy;

    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicMove();
    }

    

    private void BasicMove()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, objective.transform.position, enemy.speed * Time.deltaTime);
    }
}
