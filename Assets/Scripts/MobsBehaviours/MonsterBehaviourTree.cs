using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviourTree : AiTree
{
    protected override Node SetupTree()
    {
        //Node root = new TaskPatrol(_navMeshAgent, _animator);

        Character mob = GetComponent<Character>();

        Node root = new Selector(
                new List<Node> { 
                    new CheckEnemyInAttackRange(mob),
                    new Sequence(
                            new List<Node>{ 
                                new CheckEnemyInFieldOfView(mob),
                                new TaskGoToTarget(mob)
                            }
                        ),
                    new TaskPatrol(mob)
                }
            );

        return root;
    }
}
