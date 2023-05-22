using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the available number of spawnable Game Objects
/// * Holds the lists of Game Objects that act as our "Pool"
/// * Ability to dynamically add Game Objects to lists
/// * Method to check list and return Game Object to Spawn Manager
/// </summary>

public class PoolManager : MonoBehaviour
{
    #region Singleton
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The PoolManager is NULL.");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    #region Variables
    //Empty Parent Container for hierarchy organization
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _enemyPool;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemy(10);
    }


    /// <summary>
    /// Method to generate and populate the Enemy Pool.
    /// </summary>
    /// <param name="amountOfEnemies">The ammount of enemies wanted in the pool</param>
    /// <returns>Returns the Enemy Pool List </returns>
    List <GameObject> GenerateEnemy(int amountOfEnemies)
    {
        for(int i=0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }

    /// <summary>
    /// Checks whether an enemy within the pool is available. If not, we create a new enemy and add it to the pool to be recycled.
    /// </summary>
    /// <returns>Enemy Prefab</returns>
    public GameObject RequestEnemy()
    {
        foreach(var enemy in _enemyPool)
        {
            if(enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }
}
