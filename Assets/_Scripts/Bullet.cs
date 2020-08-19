using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    [Header("Properties")] 
    public Transform Prefab;

    public GameObject ImpactEffect;

    public float ImpactEffectDuration;

    [HideInInspector] public int Damage;

    [HideInInspector] public float critChance;

    [HideInInspector] public float critMultiplier;
    
    public float Lifetime;
    
    public float Speed;

    public float Size;

    public Vector2 SizeCollide;

    [HideInInspector] public float Angle;

    [HideInInspector] public float hitShootKnockback;

    public LayerMask CollideWith;
}