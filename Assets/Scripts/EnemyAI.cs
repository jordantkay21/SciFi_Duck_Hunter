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
    private Barrier _cover;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {

            if (_currentPoint == _wayPoints.Count - 1)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                _currentPoint++;
            }

            _agent.SetDestination(_wayPoints[_currentPoint].position);
        }

        CheckIfCovering();
    }
#region Animations

    void SetSpeed(float speed, float animationSpeed)
    {
        _agent.speed = speed;
        _animator.SetFloat("Speed", animationSpeed);
    }

    void CheckIfCovering()
    {
        if(_takingCover == true)
        {
            _animator.SetBool("Hiding", true);
            SetSpeed(0, 4);
        }
        else
        {
            _animator.SetBool("Hiding", false);
            SetSpeed(4, 4);
        }
    }

    void OnDeath()
    {
        _animator.SetTrigger("Death");
    }
    #endregion

    #region Barriers

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Barrier Detected");

        if (other.tag == "Barrier")
        {       
            _cover = other.transform.GetComponent<Barrier>();
        }
    }

    IEnumerator TakingCoverRoutine()
    {
        float time = Random.Range(1, 5);
        yield return new WaitForSeconds(time);
        _takingCover = false;
        _cover.Unoccupied();
    }

    public void TakingCover()
    {
        _takingCover = true;
        StartCoroutine(TakingCoverRoutine());
    }

    #endregion
}
