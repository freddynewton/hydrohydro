using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stat : ScriptableObject
{
    public float moveSpeed;
    public int health;

    [Header("Enemy Stats")]
    public float maxRange = 3f;
    
}
