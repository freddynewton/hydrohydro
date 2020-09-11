using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using Pathfinding;
using Data.Util;
using UnityEngine.Assertions.Must;

public class TilemapPCGHandler : MonoBehaviour
{
    public static TilemapPCGHandler Instance { get; private set; }

    [Header("Cellular Automata")]
    public CellularAutomataPCG mapGenerator;

    [Header("Tile")]
    public PCGTile tile;

    [Header("Grid Objects")]
    public GameObject grid;
    public Material litSpriteMat;
    [HideInInspector] public List<Tilemap> maps;

    [Header("Settings")]
    public bool developerMode;
    public bool loadOnStart = false;
    public WorldSettings worldSettings;

    private int[,] map;

    private void Start()
    {
        if (loadOnStart)
        {
            generate();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        worldSettings.init(maps[0]);
        StartCoroutine(bakeNavmesh());
        Playercontroller.Instance.gameObject.transform.position = getRandomPoint();
    }

    private void startFillMap()
    {
        map = mapGenerator.GenerateMap();
        Tilemap tilemap = createTilemap(true);
        maps.Add(tilemap);

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

        resetMapMatrix(tilemap);
    }

    public void resetMapMatrix(Tilemap tilemap)
    {
        int[,] newMap = new int[mapGenerator.width, mapGenerator.height];

        for (int x = 0; x < mapGenerator.width; x++)
        {
            for (int y = 0; y < mapGenerator.height; y++)
            {
                newMap[x, y] = tilemap.GetColliderType(new Vector3Int(x, y, 1)) == Tile.ColliderType.Sprite ? 1 : 0;
            }
        }
        map = newMap;
    }

    private IEnumerator bakeNavmesh()
    {
        yield return new WaitForEndOfFrame();

        GameObject pf = GameObject.FindGameObjectWithTag("Pathfinding");

        foreach (NavGraph graph in AstarPath.active.graphs)
        {
            graph.Scan();
        }
    }

    public Vector3 getRandomPoint()
    {
        Vector3 point = new Vector3();
        bool foundPoint = false;

        while (!foundPoint)
        {
            int randomIntX = Random.Range(0, mapGenerator.width);
            int randomIntY = Random.Range(0, mapGenerator.height);

            if (map[randomIntX, randomIntY] == 0)
            {
                point = new Vector3(randomIntX * 0.16f, randomIntY * 0.16f, 0);
                foundPoint = true;
            }
        }

        return point;
    }

    public Vector3 getRandomPointSqrSetWalkable(int width, int height, Tilemap tilemap)
    {
        Vector3 point = new Vector3();
        bool foundPoint = false;

        while (!foundPoint)
        {
            int randomIntX = Random.Range(0, mapGenerator.width);
            int randomIntY = Random.Range(0, mapGenerator.height);

            if (randomIntX - (width / 2) - 1 > 0 && randomIntX + (width / 2) + 1 < mapGenerator.width &&
                randomIntY - (height / 2) - 1 > 0 && randomIntY + (height / 2) + 1 < mapGenerator.height)
            {
                if (map[randomIntX, randomIntY] == 0)
                {
                    for (int x = randomIntX - 1 - width / 2; x < randomIntX + 1 + width / 2; x++)
                    {
                        for (int y = randomIntY - 1 - height / 2; y < randomIntY + 1 + height / 2; y++)
                        {
                            tilemap.SetTile(new Vector3Int(x, y, 1), tile.floorRuleTile);
                        }
                    }

                    point = new Vector3(randomIntX, randomIntY, 1);
                    foundPoint = true;
                }
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
        tilemap.layer = 8;
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
