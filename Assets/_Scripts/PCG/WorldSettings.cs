using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PCG/WorldSetting")]
public class WorldSettings : ScriptableObject
{
    [Serializable]
    public struct WorldObjects
    {
        public GameObject type;
        public int minAmount;
        public int maxAmount;
    }

    [Header("Spawn Objects")]
    public WorldObjects[] objects;

    public void init()
    {
        foreach (WorldObjects obj in objects)
        {
            for (int i = 0; i < UnityEngine.Random.Range(obj.minAmount, obj.maxAmount + 1); i++)
            {
                Instantiate(obj.type, TilemapPCGHandler.Instance.getRandomPoint(), Quaternion.identity, null);
            }
        }
    }
}
