using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 3;
    public float spawnTimer = 2f;

    public GameObject[] Enemies;

    private void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }


    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnPos, Quaternion.identity, GameObject.FindGameObjectWithTag("EnemyCollection").transform);

        yield return new WaitForSecondsRealtime(spawnTimer);
        StartCoroutine(SpawnAnEnemy());
    }
}
