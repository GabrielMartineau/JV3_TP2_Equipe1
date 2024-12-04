using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveTypeEnum : MonoBehaviour
{
    public AIMoveType aiMoveType;
}

public enum AIMoveType
{
    towardsObjective,
    singleAxis,
    doubleAxis,
    balloon
}
