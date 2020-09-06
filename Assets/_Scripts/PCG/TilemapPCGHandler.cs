    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using Pathfinding;

public class TilemapPCGHandler : MonoBehaviour
{
    [Header("Cellular Automata")]
    public CellularAutomataPCG mapGenerator;

    [Header("Tile")]
    public PCGTile tile;

    [Header("Grid Objects")]
    public GameObject grid;
    public Material litSpriteMat;
    

    [Header("Settings")]
    public bool developerMode;
    public bool loadOnStart = false;

    private int[,] map;

    private void Start()
    {
        if (loadOnStart)
        {
            generate();
        }
    }

    private void Update()
    {
        if (developerMode && Input.GetMouseButtonDown(0))
        {
            generate();
        }
    }

    public void generate()
    {
        clearMap();
        startFillMap();
        bakeNavmesh();
        spawnPlayer();
    }

    public void startFillMap()
    {
        map = mapGenerator.GenerateMap();
        Tilemap tilemap = createTilemap(true);

        for (int x = 0; x < mapGenerator.width; x++)
        {
            for (int y = 0; y < mapGenerator.height; y++)
            {
                if (map[x, y] == 0)
                {
                    // Debug.Log("Set Floor");
                    tilemap.SetTile(new Vector3Int(x, y, 1), tile.floorRuleTile);
                }
                else
                {
                    // Debug.Log("Set background");
                    tilemap.SetTile(new Vector3Int(x, y, 1), tile.backgroundTile);
                }
            }
        }
    }

    private void bakeNavmesh()
    {
        GameObject pf = GameObject.FindGameObjectWithTag("Pathfinding");
        AstarPath.active.Scan();
    }

    private void spawnPlayer()
    {
        //player.transform.position = findRandomSpawnPoint();
    }

    private Vector3 findRandomSpawnPoint()
    {
        Vector3 point = new Vector3();
        bool foundPoint = false;

        while (!foundPoint)
        {
            int randomIntX = Random.Range(0, mapGenerator.width);
            int randomIntY = Random.Range(0, mapGenerator.height);

            if (map[randomIntX, randomIntY] == 0 && map[randomIntX + 1, randomIntY] != 1 && map[randomIntX - 1, randomIntY] != 1 && map[randomIntX, randomIntY - 1] != 1 && map[randomIntX, randomIntY - 0] != 1)
            {
                point = new Vector3(randomIntX, randomIntY, 0);
                foundPoint = true;
            }
        }

        return point;
    }

    private void clearMap()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private Tilemap createTilemap(bool haveCollider)
    {
        GameObject tilemap = new GameObject();
        Tilemap Map = tilemap.AddComponent<Tilemap>();
        TilemapRenderer renderer = tilemap.AddComponent<TilemapRenderer>();
        renderer.material = litSpriteMat;
        tilemap.name = "Tilemap" + grid.transform.childCount;

        if (haveCollider)
            tilemap.AddComponent<TilemapCollider2D>();

        renderer.sortingOrder = grid.transform.childCount;
        Map.transform.parent = grid.transform;

        return Map;
    }
}
