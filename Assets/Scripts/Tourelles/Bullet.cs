using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trackedEnemy;

    [SerializeField] private GameObject model;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource sfx;

    public BulletType bulletType;

    private bool isActive;

    void Start()
    {
        isActive = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if(trackedEnemy != null)
            {
                if(!trackedEnemy.GetComponent<BasicEnemyAI>().isAlive)
                {
                    trackedEnemy = null;
                }
                else
                {
                    Vector3 targetForward = Vector3.Lerp(gameObject.transform.forward, Vector3.Normalize(trackedEnemy.transform.position - gameObject.transform.position), bulletType.rotateSpeed * Time.deltaTime);
                    Quaternion targetQuaternion = Quaternion.LookRotation(targetForward);
                    gameObject.GetComponent<Rigidbody>().MoveRotation(targetQuaternion);
                }
            }

            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + gameObject.transform.forward * bulletType.moveSpeed * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        KillBullet();
    }

    private void KillBullet()
    {
        isActive = false;
        model.SetActive(false);
        particles.SetActive(true);
        sfx.PlayOneShot(sfx.clip);
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<Collider>());
        Invoke("DestroySelf", 2f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
