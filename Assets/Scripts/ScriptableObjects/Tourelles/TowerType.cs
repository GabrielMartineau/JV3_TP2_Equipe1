using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerType", menuName = "SO/TowerType")]
public class TowerType : ScriptableObject
{
    public float fireCooldown;
    public float bulletsPerShot;
    public GameObject bullet;
    public float rotateSpeed;
    public float maxHeadAngle;
}
