using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;
enum Direction { Left, Right, Top, Bottum }

public class PCGenerator : MonoBehaviour
{
    [Header("Default Settings")]
    public bool startOnStart;
    public float waitTimeForTileSpawning = 0.01f;

    [Header("Tiles")]
    public PCGTile tile;

    [Header("Spawn Object Parents")]
    public GameObject walkable;
    public GameObject notWalkable;
    public GameObject others;

    [Header("Grid")]
    public Grid grid;

    [Header("MapSize")]
    public int rooms;
    public int roomSizeMin;
    public int RoomSizeMax;

    private void Start()
    {
        if (startOnStart)
            startPCG();
    }

    public void startPCG()
    {
        System.Diagnostics.Stopwatch.StartNew();
        clearMap();
        //fillBackground();
        StartCoroutine(initPCG());
    }

    private IEnumerator initPCG()
    {
        Tilemap Map = createTilemap();

        int countRoomsPlaced = 0;

        while (countRoomsPlaced < rooms)
        {
            int countTilePlaced = 0;
            int random = UnityEngine.Random.Range(roomSizeMin, RoomSizeMax);
            List<Vector3Int> placedTiles = new List<Vector3Int>();
            placedTiles.Add(new Vector3Int(0, 0, 0));

            while (countTilePlaced < random)
            {
                Vector3Int pos = placedTiles[UnityEngine.Random.Range(0, placedTiles.Count)];
                bool placed = false;
                
                if (!Map.HasTile(pos))
                    Map.SetTile(pos, tile.floorRuleTile);
                else
                {
                    Direction dir = giveDirection();

                    if (dir == Direction.Left && !Map.HasTile(pos + new Vector3Int(-1, 0, 0)))
                    {
                        pos += new Vector3Int(-1, 0, 0);
                        Map.SetTile(pos, tile.floorRuleTile);
                        placedTiles.Add(pos);
                        placed = true;
                    }
                    if (dir == Direction.Right && !Map.HasTile(pos + new Vector3Int(1, 0, 0)))
                    {
                        pos += new Vector3Int(1, 0, 0);
                        Map.SetTile(pos, tile.floorRuleTile);
                        placedTiles.Add(pos);
                        placed = true;
                    }
                    if (dir == Direction.Top && !Map.HasTile(pos + new Vector3Int(0, 1, 0)))
                    {
                        pos += new Vector3Int(0, 1, 0);
                        Map.SetTile(pos, tile.floorRuleTile);
                        placedTiles.Add(pos);
                        placed = true;
                    }
                    if (dir == Direction.Bottum && !Map.HasTile(pos + new Vector3Int(0, -1, 0)))
                    {
                        pos += new Vector3Int(0, -1, 0);
                        Map.SetTile(pos, tile.floorRuleTile);
                        placedTiles.Add(pos);
                        placed = true;
                    }
                }
                yield return new WaitForSeconds(waitTimeForTileSpawning);

                if (placed)
                {
                    countTilePlaced++;
                    Debug.Log("Tiles placed: " + countTilePlaced);
                }
            }
            countRoomsPlaced++;
        }

    }

    /*
    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap.SetTilesBlock.html
    /// </summary>
    private void fillBackground()
    {
        Tilemap Map = createTilemap();

        TileBase[] tileArray = new TileBase[tile.bounds.size.x * tile.bounds.size.y * tile.bounds.size.z];
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = tile.backgroundTile;
        }

        Map.SetTilesBlock(tile.bounds, tileArray);
    }
    */

    private Tilemap createTilemap()
    {
        GameObject tilemap = new GameObject();
        Tilemap Map = tilemap.AddComponent<Tilemap>();
        TilemapRenderer renderer = tilemap.AddComponent<TilemapRenderer>();
        tilemap.name = "Tilemap" + grid.transform.childCount;

        renderer.sortingOrder = grid.transform.childCount;
        Map.transform.parent = grid.transform;

        return Map;
    }

    private Direction giveDirection()
    {
        int random = UnityEngine.Random.Range(0, 4);

        switch (random)
        {
            case 0:
                return Direction.Left;
            case 1:
                return Direction.Right;
            case 2:
                return Direction.Top;
            case 3:
                return Direction.Bottum;
        }

        return Direction.Right;
    }

    private void clearMap()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
    }


}
