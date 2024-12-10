using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    private List<GameObject> enemiesInRange;
    private float cooldown;

    [SerializeField] private TowerType towerType;

    private GameObject bullet;

    [SerializeField] private List<Transform> bulletSpawnpoints;

    [SerializeField] private GameObject model;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject aimPoint;

    private int activeSpawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        bullet = towerType.bullet;
        cooldown = 0f;
        enemiesInRange = new List<GameObject>();
        activeSpawnpoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesInRange.Count > 0)
        {
            
            if(enemiesInRange[0] == null || enemiesInRange[0].GetComponent<BasicEnemyAI>().isAlive == false)
            {
                enemiesInRange.Remove(enemiesInRange[0]);
            }
            else
            {
                LookAtTarget();
            }
        }

        if(enemiesInRange.Count > 0 && cooldown <= 0f)
        {
            Attack();
        }

        cooldown -= Time.deltaTime;
        if(cooldown < 0f) cooldown = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void LookAtTarget()
    {
        Vector3 modelTargetForward = Vector3.Slerp(model.transform.forward, Vector3.Normalize(enemiesInRange[0].transform.position - model.transform.position), towerType.rotateSpeed * Time.deltaTime);
        modelTargetForward.y = 0;

        Quaternion modelTargetQuaternion = Quaternion.LookRotation(modelTargetForward);

        model.GetComponent<Rigidbody>().MoveRotation(modelTargetQuaternion);

        Vector3 headTargetForward = Vector3.Slerp(aimPoint.transform.forward, Vector3.Normalize(enemiesInRange[0].transform.position - aimPoint.transform.position), towerType.rotateSpeed * Time.deltaTime);

        if(Vector3.Angle(model.transform.forward, headTargetForward) > towerType.maxHeadAngle)
        {
            if(headTargetForward.y > 0)
            {
                headTargetForward = Vector3.Slerp(model.transform.forward, Vector3.up, towerType.maxHeadAngle/90f);
            }
            else
            {
                headTargetForward = Vector3.Slerp(model.transform.forward, Vector3.down, towerType.maxHeadAngle/90f);
            }
            
        }
        

        Quaternion headTargetQuaternion = Quaternion.LookRotation(headTargetForward);

        head.GetComponent<Rigidbody>().MoveRotation(headTargetQuaternion);
        
    }

    private void Attack()
    {
        model.GetComponent<Animator>().SetTrigger("Shoot");

        for(int i = 0; i < towerType.bulletsPerShot; i++)
        {
            GameObject newBullet = Instantiate(bullet, gameObject.transform);

            if(newBullet.GetComponent<Bullet>() != null)
            {
                newBullet.GetComponent<Bullet>().trackedEnemy = enemiesInRange[0];
                newBullet.transform.position = bulletSpawnpoints[activeSpawnpoint].transform.position;

                Quaternion rotation = Quaternion.LookRotation(bulletSpawnpoints[activeSpawnpoint].forward);
                newBullet.GetComponent<Rigidbody>().MoveRotation(rotation);
            }
            else if(newBullet.GetComponent<LaserBeam>() != null)
            {
                newBullet.GetComponent<LaserBeam>().origin = bulletSpawnpoints[activeSpawnpoint];
            }

            bulletSpawnpoints[activeSpawnpoint].GetChild(0).GetComponent<ParticleSystem>().Play();

            activeSpawnpoint++;
            if(activeSpawnpoint >= bulletSpawnpoints.Count) activeSpawnpoint = 0;
        }

        cooldown = towerType.fireCooldown;
    }
}
