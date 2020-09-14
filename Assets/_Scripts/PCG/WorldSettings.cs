using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "PCG/WorldSetting")]
public class WorldSettings : ScriptableObject
{
    [Serializable]
    public struct WorldObjects
    {
        public WorldObjectPlacement type;
        public int minAmount;
        public int maxAmount;
    }

    [Header("Spawn Objects")]
    public WorldObjects[] objects;

    public void init(Tilemap tilemap)
    {
        foreach (WorldObjects obj in objects)
        {
            for (int i = 0; i < UnityEngine.Random.Range(obj.minAmount, obj.maxAmount + 1); i++)
            {
                Vector3 pos = TilemapPCGHandler.Instance.getRandomPointSqrSetWalkable(obj.type.width, obj.type.height, tilemap);
                
                foreach (GameObject _obj in obj.type.objects)
                {
                    bool set = false;

                    while (!set)
                    {
                        int randomx = (int)UnityEngine.Random.Range(pos.x - obj.type.width / 2 - 1, pos.x + obj.type.width / 2 - 1);
                        int randomy = (int)UnityEngine.Random.Range(pos.y - obj.type.height / 2 - 1, pos.y + obj.type.height / 2 - 1);

                        if (TilemapPCGHandler.Instance.map[randomx, randomy] == 2)
                        {
                            TilemapPCGHandler.Instance.map[randomx, randomy] = 3;
                            Instantiate(_obj, new Vector2(randomx * 0.16f, randomy * 0.16f), Quaternion.identity, null);
                            set = true;
                        }
                    }
                }
            }
        }

        TilemapPCGHandler.Instance.resetMapMatrix(tilemap);
    }
}
