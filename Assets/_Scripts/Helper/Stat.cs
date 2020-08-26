using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stat : ScriptableObject
{
    public float moveSpeed;
    public int health;

    [Header("Weapon Stats")]
    public float weaponLookAtSpeed = 200f;

    [Header("Enemy Stats")]
    public float maxRange = 3f;
    
    
}
