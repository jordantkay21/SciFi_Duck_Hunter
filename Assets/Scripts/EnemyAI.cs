using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls Enemy AI Logic
/// * Run - Intelligently select barriers to run and hide behind [Should always be moving forward]
/// * Hide - Ability to stop running when they are at their selected barrier for a random amount of time
/// * Death - Trigger Death when AI is shot down | Communicate with ANimation Scripts | Award 50 points to be Awarded
/// </summary>

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _wayPoints;
    private NavMeshAgent _agent;
    private Animator _animator;
    private int _currentPoint = 0;

    private bool _takingCover = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("EnemyAI did not detect Animator");
        }

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("EnemyAI did not detect NavMesh Agent");
        }

        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
        }
        SetAnimatorSpeed(4);
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

        CheckIfCovering();
    }

    void SetAnimatorSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
        _agent.speed = speed;
    }

    void CheckIfCovering()
    {
        if(_takingCover == true)
        {
            _animator.SetBool("Cover_Idle", true);
        }
        else
        {
            _animator.SetBool("Cover_Idle", false);
        }
    }

    void OnDeath()
    {
        _animator.SetTrigger("Death");
    }
}
