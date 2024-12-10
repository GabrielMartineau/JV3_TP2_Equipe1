using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserBeam : MonoBehaviour
{

    public BulletType bulletType;
    public Transform origin;

    [SerializeField] private GameObject model;

    private bool isFading;

    private float fadingProcess;
    private float width;
    private float colliderRadius;
    private Rigidbody rb;

    void Start()
    {
        isFading = false;
        fadingProcess = 1;
        rb = gameObject.GetComponent<Rigidbody>();

        width = model.GetComponent<VolumetricLineBehavior>().LineWidth;
        colliderRadius = gameObject.GetComponent<CapsuleCollider>().radius;

        Invoke("KillBullet", bulletType.moveSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        if(isFading)
        {
            fadingProcess -= bulletType.rotateSpeed * Time.deltaTime;

            model.GetComponent<VolumetricLineBehavior>().LineWidth = width * fadingProcess;
            gameObject.GetComponent<CapsuleCollider>().radius = colliderRadius * fadingProcess;
        }

        rb.MovePosition(origin.position);
        rb.MoveRotation(origin.rotation);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BasicEnemyAI>().LoseHP(bulletType.damage);
        }
    }

    private void KillBullet()
    {
        isFading = true;
        Invoke("DestroySelf", 1 / bulletType.rotateSpeed);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
