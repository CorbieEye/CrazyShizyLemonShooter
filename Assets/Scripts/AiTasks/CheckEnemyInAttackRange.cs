using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckEnemyInAttackRange : Node
{
    private Character _mob;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private MobData _mobData;

    public CheckEnemyInAttackRange(Character mob) {
        _mob = mob;
        _navMeshAgent = _mob.GetComponent<NavMeshAgent>();
        _animator = _mob.GetComponent<Animator>();
        _mobData = _mob.GetComponent<MobData>();
    }

    public override NodeState Evaluate()
    {
        object playerCharacterObj = GetData("PlayerCharacter");
        var playerCharacter = (Character)playerCharacterObj;
        if (playerCharacter == null) {
            _state = NodeState.FAILURE;
            return _state;
        }

        if (Vector3.Distance(_navMeshAgent.gameObject.transform.position, playerCharacter.gameObject.transform.position) <= _mobData.DistanceToStopToEnemy) {
            _animator.SetBool("Attack", true);
            _animator.SetBool("Move", false);
            
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.FAILURE;
        return _state;
    }
}
