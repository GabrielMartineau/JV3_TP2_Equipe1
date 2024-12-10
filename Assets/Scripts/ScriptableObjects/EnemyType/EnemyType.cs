using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "SO/EnemyType")]
public class EnemyType : ScriptableObject
{
    public float speed;
    public float maxHP;

    public AIMoveType aiMoveType;

    public int score;
}
