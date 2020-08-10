using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    [Header("Properties")] 
    public Transform Prefab;

    public float Damage;
    
    public float Lifetime;
    
    public float Speed;

    public float Size;

    public LayerMask CollideWith;
}