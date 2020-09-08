using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 3;
    public float spawnTimer = 2f;

    public GameObject[] Enemies;

    private void Awake()
    {
        StartCoroutine(SpawnAnEnemy());
    }


    IEnumerator SpawnAnEnemy()
    {
        bool foundP = false;

        while (!foundP)
        {
            Vector2 p = TilemapPCGHandler.Instance.getRandomPoint();
            if (Vector2.Distance(transform.position, p) <= spawnRadius)
            {
                foundP = true;
                Instantiate(Enemies[Random.Range(0, Enemies.Length)], p, Quaternion.identity, GameObject.FindGameObjectWithTag("EnemyCollection").transform);
            }
        }

        yield return new WaitForSecondsRealtime(spawnTimer);
        StartCoroutine(SpawnAnEnemy());
    }
}
