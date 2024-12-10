using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BasicEnemyAI : MonoBehaviour
{
    public GameObject objective;
    [SerializeField] private EnemyType enemy;

    public float hP;

    private Vector3 axis1;
    private Vector3 axis2;
    private Vector3 balloonVector;
    private Rigidbody rb;
    private Collider collider;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject zapParticle;
    [SerializeField] private AudioSource deathSFX;
    public GestionnaireVagues waveManager;
    public GestionnaireScore scoreManager;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        hP = enemy.maxHP;
        rb = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();
        gameObject.GetComponent<LookAtConstraint>().SetSource(0, transform.parent.GetComponent<LookAtConstraint>().GetSource(0));
        isAlive = true;

        switch(enemy.aiMoveType)
        {
            case (AIMoveType)1 : InvokeRepeating("SingleAxisRoutine", 0f, 1f); break;
            case (AIMoveType)2 : InvokeRepeating("DoubleAxisRoutine", 0f, 1f); break;
            case (AIMoveType)3 : BalloonRandomizer(); break;
            default : break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 finalMove = Vector3.zero;
        
        if(isAlive)
        {
            switch(enemy.aiMoveType)
            {
                case (AIMoveType)0 : finalMove += FindPath(); break;
                case (AIMoveType)1 : finalMove += axis1; break;
                case (AIMoveType)2 : finalMove += (axis1 + axis2) / 2; break;
                case (AIMoveType)3 : finalMove += (FindPath() * 2 + balloonVector) / 3; break;
                default : break;
            }

            rb.MovePosition(gameObject.transform.position + finalMove * enemy.speed * Time.deltaTime);
        }
    }

    private Vector3 FindPath()
    {
        Debug.Log("FindPath() launched");
        return Vector3.Normalize(objective.transform.position - gameObject.transform.position);
    }


    

    private void SingleAxisRoutine()
    {
        if(Vector3.Distance(objective.transform.position, gameObject.transform.position) <= 0.5f)
        {
            axis1 = FindPath();
        }
        else
        {
            int directionID = Random.Range(0, 3);
            switch(directionID)
            {
                case 0 : axis1 = Vector3.Normalize(new Vector3(objective.transform.position.x - gameObject.transform.position.x, 0, 0)); break;
                case 1 : axis1 = Vector3.Normalize(new Vector3(0, objective.transform.position.y - gameObject.transform.position.y, 0)); break;
                case 2 : axis1 = Vector3.Normalize(new Vector3(0, 0, objective.transform.position.z - gameObject.transform.position.z)); break;
                default : break;
            }
            if(axis1 == Vector3.zero)
            {
                switch(directionID)
                {
                    case 0 : axis1 = Vector3.left; break;
                    case 1 : axis1 = Vector3.up; break;
                    case 2 : axis1 = Vector3.forward; break;
                    default : break;
                }

            }
        }
    }

    private void DoubleAxisRoutine()
    {
        if(Vector3.Distance(objective.transform.position, gameObject.transform.position) <= 0.5f)
        {
            axis1 = FindPath();
            axis2 = FindPath();
        }
        else
        {
            int directionID = Random.Range(0, 3);
            switch(directionID)
            {
                case 0 : axis1 = Vector3.Normalize(new Vector3(objective.transform.position.x - gameObject.transform.position.x, 0, 0)); break;
                case 1 : axis1 = Vector3.Normalize(new Vector3(0, objective.transform.position.y - gameObject.transform.position.y, 0)); break;
                case 2 : axis1 = Vector3.Normalize(new Vector3(0, 0, objective.transform.position.z - gameObject.transform.position.z)); break;
                default : break;
            }
            if(axis1 == Vector3.zero)
            {
                switch(directionID)
                {
                    case 0 : axis1 = Vector3.left; break;
                    case 1 : axis1 = Vector3.up; break;
                    case 2 : axis1 = Vector3.forward; break;
                    default : break;
                }

            }

            directionID = Random.Range(0, 3);
            switch(directionID)
            {
                case 0 : axis2 = Vector3.Normalize(new Vector3(objective.transform.position.x - gameObject.transform.position.x, 0, 0)); break;
                case 1 : axis2 = Vector3.Normalize(new Vector3(0, objective.transform.position.y - gameObject.transform.position.y, 0)); break;
                case 2 : axis2 = Vector3.Normalize(new Vector3(0, 0, objective.transform.position.z - gameObject.transform.position.z)); break;
                default : break;
            }
            if(axis2 == Vector3.zero)
            {
                switch(directionID)
                {
                    case 0 : axis2 = Vector3.left; break;
                    case 1 : axis2 = Vector3.up; break;
                    case 2 : axis2 = Vector3.forward; break;
                    default : break;
                }

            }
        }
    }

    

    private void BalloonRandomizer()
    {
        balloonVector = Vector3.Cross(FindPath(), Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))));
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.CompareTag("Bullet") && other.collider.gameObject.GetComponent<Bullet>() != null)
        {
            LoseHP(other.collider.gameObject.GetComponent<Bullet>().bulletType.damage);
        }
    }

    public void LoseHP(float damage)
    {
        hP -= damage;
        if(hP <= 0 && isAlive) KillEnemy();
    }

    private void KillEnemy()
    {
        isAlive = false;
        zapParticle.SetActive(true);
        deathSFX.PlayOneShot(deathSFX.clip);
        Destroy(rb);
        Destroy(collider);
        Destroy(model);
        scoreManager.EnemyScore(enemy.score);
        waveManager.VerifVagueTermine();
        Invoke("DestroySelf", 3f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
