using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _wayPoints;
    private NavMeshAgent _agent;
    private int _currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("EnemyAI did not detect NavMesh Agent");
        }

        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            if (_currentPoint == _wayPoints.Count - 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _currentPoint++;
            }

            _agent.SetDestination(_wayPoints[_currentPoint].position);
        }
    }
}
