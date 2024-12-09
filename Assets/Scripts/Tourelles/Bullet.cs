using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trackedEnemy;

    private Vector3 newForward;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource sfx;
    
    public int damage;


    // Update is called once per frame
    void Update()
    {
        if(trackedEnemy != null)
        {
            if(!trackedEnemy.GetComponent<BasicEnemyAI>().isAlive)
            {
                trackedEnemy = null;
            }
            else
            {
                Vector3 targetForward = Vector3.Lerp(gameObject.transform.forward, Vector3.Normalize(trackedEnemy.transform.position - gameObject.transform.position), rotationSpeed * Time.deltaTime);
                Quaternion targetQuaternion = Quaternion.LookRotation(targetForward);
                gameObject.GetComponent<Rigidbody>().MoveRotation(targetQuaternion);
            }
        }

        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + gameObject.transform.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        KillBullet();
    }

    private void KillBullet()
    {
        Destroy(gameObject);
    }
}
