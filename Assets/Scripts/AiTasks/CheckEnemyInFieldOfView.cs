using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckEnemyInFieldOfView : Node
{
    private Character _mob;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private MobData _mobData;

    public CheckEnemyInFieldOfView(Character mob) {
        _mob = mob;
        _navMeshAgent = _mob.GetComponent<NavMeshAgent>();
        _animator = _mob.GetComponent<Animator>();
        _mobData = _mob.GetComponent<MobData>();
    }

    public override NodeState Evaluate()
    {
        object player = GetData("PlayerCharacter");
        if (player == null) {
            try
            {
                Character playerCharacter;
                if (_mobData.Aggressor != null)
                {
                    playerCharacter = _mobData.Aggressor;
                }
                else
                {
                    var playerCharacterController = Object.FindObjectsOfType(typeof(PlayerCharacterController))[0] as PlayerCharacterController;
                    playerCharacter = playerCharacterController.gameObject.GetComponent<Character>();
                    if (Vector3.Distance(_navMeshAgent.gameObject.transform.position, playerCharacter.gameObject.transform.position) > _mobData.ViewDistance)
                    {
                        throw new System.Exception();
                    }
                }

                Parent.Parent.SetData("PlayerCharacter", playerCharacter);
                _animator.SetBool("Move", true);

                _state = NodeState.SUCCESS;
                return _state;
            }
            catch {
                _state = NodeState.FAILURE;
                return _state;
            }
        }
        _state = NodeState.SUCCESS;
        return _state;
    }
}
