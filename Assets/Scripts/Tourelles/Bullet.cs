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
        if(trackedEnemy != null) path = FindPath();
        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + path * Time.deltaTime);

        if(Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position) >= 5f)
        {
            KillBullet();
        }
    }

    private Vector3 FindPath()
    {
        return Vector3.Normalize(trackedEnemy.transform.position - gameObject.transform.position);
    }

    public void KillBullet()
    {
        Destroy(gameObject);
    }
}
