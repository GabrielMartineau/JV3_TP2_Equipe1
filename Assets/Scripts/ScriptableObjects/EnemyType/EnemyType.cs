using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "SO/EnemyType")]
public class EnemyType : ScriptableObject
{
    public float speed;
    public int maxHP;

    public bool moveTowardsObjective;
    public bool moveOnAxis;
}
