using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "PCG/WorldObjectPlacement", fileName = "WorldObjectPlacement")]
public class WorldObjectPlacement : ScriptableObject
{
    [Header("Size")]
    public int width;
    public int height;

    [Header("Background Tile")]
    public Tile backgroundTile;


    [Header("Objects")]
    public GameObject[] objects;



}
