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

    private Vector3 enemyStartPos = new Vector3(31, 0, 1);

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject enemy = PoolManager.Instance.RequestEnemy();
            enemy.transform.position = enemyStartPos;
        }
    }

    
}
