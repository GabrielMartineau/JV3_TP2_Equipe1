using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKiller : MonoBehaviour
{
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet>().trackedEnemy == null) other.gameObject.GetComponent<Bullet>().KillBullet();
    }
}
