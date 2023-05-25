using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls WHEN spawnable Game Objects spawn and WHERE they spawn at
/// * Needs to request a new Game Object from the Pool Manager
/// </summary>

public class SpawnManager : MonoBehaviour
{

    private bool _stopSpawning = false;
    private Vector3 enemyStartPos = new Vector3(35, 0, 0);

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForEndOfFrame();
        while (_stopSpawning == false)
        {
            GameObject enemy = PoolManager.Instance.RequestEnemy();
            enemy.transform.position = enemyStartPos;
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }

    }

    
}
