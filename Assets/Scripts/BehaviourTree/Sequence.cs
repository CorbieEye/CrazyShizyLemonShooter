using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() : base () { }
    public Sequence(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildIsRunning = false;
        foreach (var child in _children) {
            switch (child.Evaluate()) {
                case NodeState.FAILURE:
                    _state = NodeState.FAILURE;
                    return _state;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNUNG:
                    anyChildIsRunning = true;
                    continue;
                default:
                    _state = NodeState.SUCCESS;
                    return _state;
            }
        }
        _state = anyChildIsRunning ? NodeState.RUNNUNG : NodeState.SUCCESS;


        return _state;
    }
}
