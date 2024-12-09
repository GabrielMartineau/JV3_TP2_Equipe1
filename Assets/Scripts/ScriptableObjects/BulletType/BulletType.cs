using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletType", menuName = "SO/BulletType")]
public class BulletType : ScriptableObject
{
    public int damage;
    public float moveSpeed;
    public float rotateSpeed;
}
