using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    private List<GameObject> enemiesInRange;
    private float cooldown;

    [SerializeField] private TowerType towerType;

    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        enemiesInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesInRange[0] == null)
        {
            enemiesInRange.Remove(enemiesInRange[0]);
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

    private void Attack()
    {
        Debug.Log("Attack() was called!");
        GameObject newBullet = bullet;
        
        newBullet.GetComponent<Bullet>().trackedEnemy = enemiesInRange[0];
        Instantiate(newBullet, gameObject.transform);

        cooldown = towerType.fireCooldown;
    }
}
