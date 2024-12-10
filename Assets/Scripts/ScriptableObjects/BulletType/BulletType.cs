using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletType", menuName = "SO/BulletType")]
public class BulletType : ScriptableObject
{
    public float damage;
    public float moveSpeed;
    public float rotateSpeed;
}
