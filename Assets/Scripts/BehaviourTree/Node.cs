using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //
    protected NodeState _state;
    public Node Parent;
    protected List<Node> _children = new List<Node>();
    private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    public Node() {
        Parent = null;
    }

    public Node(List<Node> children) {
        foreach (var node in children) {
            Attach(node);
        } 
    }

    private void Attach(Node node) {
        node.Parent = this;
        _children.Add(node);
    }

    public virtual NodeState Evaluate() {
        return NodeState.FAILURE;
    }

    public void SetData(string key, object value) {
        _dataContext[key] = value;
    }

    public object GetData(string key) {
        object value = null;
        if (_dataContext.TryGetValue(key, out value)) {
            return value;
        }
        Node node = Parent;
        while (node != null) {
            value = node.GetData(key);
            if (value != null) {
                return value;
            }
            node = node.Parent;
        }
        return null;
    }

    public bool ClearData(string key) {
        if (_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }
        Node node = Parent;
        while (node != null)
        {
            bool cleared = node.ClearData(key);
            if (cleared)
            {
                return true;
            }
            node = node.Parent;
        }
        return false;
    }
}
