using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="PCG/Tile")]
public class PCGTile : ScriptableObject
{
    public TileBase floorRuleTile;

    [Header("Background")]
    public TileBase backgroundTile;
    //public BoundsInt bounds;
}
