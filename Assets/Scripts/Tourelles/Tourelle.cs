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
            LookAtTarget();
            if(enemiesInRange[0] == null || enemiesInRange[0].GetComponent<BasicEnemyAI>().isAlive == false)
            {
                enemiesInRange.Remove(enemiesInRange[0]);
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
        Vector3 modelTargetForward = Vector3.Lerp(model.transform.forward, Vector3.Normalize(enemiesInRange[0].transform.position - model.transform.position), towerType.rotateSpeed * Time.deltaTime);
        modelTargetForward.y = 0;

        Quaternion modelTargetQuaternion = Quaternion.LookRotation(modelTargetForward);

        model.GetComponent<Rigidbody>().MoveRotation(modelTargetQuaternion);

        Vector3 headTargetForward = Vector3.Lerp(bulletSpawnpoints[activeSpawnpoint].transform.forward, Vector3.Normalize(enemiesInRange[0].transform.position - bulletSpawnpoints[activeSpawnpoint].transform.position), towerType.rotateSpeed * Time.deltaTime);
        headTargetForward.x = 0;

        if(headTargetForward.z < 0) headTargetForward.z = -headTargetForward.z;

        if(Vector3.Angle(Vector3.forward, headTargetForward) > towerType.maxHeadAngle)
        {
            headTargetForward = Vector3.Lerp(Vector3.forward, Vector3.up, towerType.maxHeadAngle / 90f);
        }
        Quaternion headTargetQuaternion = Quaternion.LookRotation(headTargetForward);

        head.GetComponent<Rigidbody>().MoveRotation(modelTargetQuaternion * headTargetQuaternion);
        
    }

    private void Attack()
    {
        model.GetComponent<Animator>().SetTrigger("Shoot");

        GameObject newBullet = Instantiate(bullet, gameObject.transform);

        newBullet.GetComponent<Bullet>().trackedEnemy = enemiesInRange[0];
        newBullet.transform.position = bulletSpawnpoints[activeSpawnpoint].transform.position;
        newBullet.transform.Rotate(bulletSpawnpoints[activeSpawnpoint].transform.eulerAngles);

        bulletSpawnpoints[activeSpawnpoint].GetChild(0).GetComponent<ParticleSystem>().Play();

        activeSpawnpoint++;
        if(activeSpawnpoint >= bulletSpawnpoints.Count) activeSpawnpoint = 0;

        cooldown = towerType.fireCooldown;
    }
}
