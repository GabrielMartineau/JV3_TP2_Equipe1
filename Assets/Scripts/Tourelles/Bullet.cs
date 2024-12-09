using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trackedEnemy;

    private Vector3 path;

    // Update is called once per frame
    void Update()
    {
        if(trackedEnemy != null) path = FindPath(); else Invoke("KillBullet", 3f);
        if(path == Vector3.zero) path = Vector3.up;
        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + path * Time.deltaTime);
    }

    private Vector3 FindPath()
    {
        return Vector3.Normalize(trackedEnemy.transform.position - gameObject.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        KillBullet();
    }

    public void KillBullet()
    {
        Destroy(gameObject);
    }
}
