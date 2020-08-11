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

    public int Damage;
    
    public float Lifetime;
    
    public float Speed;

    public float Size;

    public Vector2 SizeCollide;

    [HideInInspector] public float Angle;

    public LayerMask CollideWith;
}