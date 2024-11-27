using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject objective;
    [SerializeField] private EnemyType enemy;

    public int hP;

    private int axis;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        hP = enemy.maxHP;
        rb = gameObject.GetComponent<Rigidbody>();

        if(enemy.moveOnAxis) InvokeRepeating("ChangeAxis", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 path = FindPath();

        Vector3 finalMove = Vector3.zero;
        

        if(enemy.moveTowardsObjective) finalMove += path;

        if(enemy.moveOnAxis) finalMove += MoveOnAxis();

        rb.MovePosition(gameObject.transform.position + finalMove * enemy.speed * Time.deltaTime);
    }

    private Vector3 FindPath()
    {
        return Vector3.Normalize(objective.transform.position - gameObject.transform.position);
    }


    private Vector3 MoveOnAxis()
    {
        switch(axis)
        {
            case 1 : return Vector3.up;
            case 2 : return Vector3.down;
            case 3 : return Vector3.left;
            case 4 : return Vector3.right;
            case 5 : return Vector3.forward;
            default : return Vector3.back;
        };
            
    }

    private void ChangeAxis()
    {
        axis = Random.Range(0, 5);
    }
}
