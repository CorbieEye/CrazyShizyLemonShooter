using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Character _mob;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private MobData _mobData;

    public TaskGoToTarget(Character mob) {
        _mob = mob;
        _navMeshAgent = _mob.GetComponent<NavMeshAgent>();
        _animator = _mob.GetComponent<Animator>();
        _mobData = _mob.GetComponent<MobData>();
    }

    public override NodeState Evaluate()
    {
        var playerCharacterObj = GetData("PlayerCharacter");
        if (playerCharacterObj != null) {
            var playerCharacter = ((Character)playerCharacterObj);
            if (Vector3.Distance(_navMeshAgent.gameObject.transform.position, playerCharacter.gameObject.transform.position) > _mobData.DistanceToStopToEnemy + 0.1f) {
                _navMeshAgent.SetDestination(((Character)playerCharacter).gameObject.transform.position);
                _navMeshAgent.stoppingDistance = _mobData.DistanceToStopToEnemy - 0.1f;
                _animator.SetBool("Move", true);
                _animator.SetFloat("Blend", 1f);
            }
        }

        _state = NodeState.RUNNUNG;
        return _state;
    }
}
