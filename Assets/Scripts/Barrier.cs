using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private bool _isOccupied = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy Detected");
        if (other.tag == "Enemy")
        {
            EnemyAI enemy = other.transform.GetComponent<EnemyAI>();

            if(enemy != null && _isOccupied == false)
            {
                _isOccupied = true;
                enemy.TakingCover();
            }
        }
    }

    public void Unoccupied()
    {
        _isOccupied = false;
    }
}
