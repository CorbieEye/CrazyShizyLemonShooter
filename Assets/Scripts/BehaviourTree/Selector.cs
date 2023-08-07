using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() : base() { }
    public Selector(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (var child in _children)
        {
            switch (child.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    _state = NodeState.SUCCESS;
                    return _state;
                case NodeState.RUNNUNG:
                    _state = NodeState.RUNNUNG;
                    return _state;
                default:
                    continue;
            }
        }
        _state = NodeState.FAILURE;

        return _state;
    }
}
