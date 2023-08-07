using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    private Character _mob;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private MobData _mobData;

    private bool _isWaiting = false;
    private float _waitCounter;
    private float _waitTime = 1f;
    private Vector3 _currentTargetPoint;

    public TaskPatrol(Character mob) {
        _mob = mob;
        _navMeshAgent = _mob.GetComponent<NavMeshAgent>();
        _animator = _mob.GetComponent<Animator>();
        _mobData = _mob.GetComponent<MobData>();
        _currentTargetPoint = GetRandomPoint(20f);
    }

    public override NodeState Evaluate()
    {
        _navMeshAgent.stoppingDistance = _mobData.DistanceToStopToPointToGo;
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _isWaiting = false;
            }
        }
        else
        {
            if (Vector3.Distance(_navMeshAgent.gameObject.transform.position, _currentTargetPoint) <= _navMeshAgent.stoppingDistance + 0.01f)
            {
                _waitCounter = 0f;
                _isWaiting = true;
                _currentTargetPoint = GetRandomPoint(20f);
                _animator.SetBool("Move", false);
            }
            else
            {
                _navMeshAgent.SetDestination(_currentTargetPoint);
                _animator.SetBool("Move", true);
                _animator.SetFloat("Blend", 0f);
            }
        }
        _state = NodeState.RUNNUNG;
        return _state;
    }

    private Vector3 GetRandomPoint(float walkRadius) {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        Debug.Log("randomDirection: " + randomDirection);

        randomDirection += _navMeshAgent.gameObject.transform.position;
        Debug.Log("randomDirection += _navMeshAgent.gameObject.transform.position: " + randomDirection);

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, -1);
        Vector3 finalPosition = hit.position;

        Debug.Log("finalPosition: " + finalPosition);

        return finalPosition;
    }
}
