using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiTree : MonoBehaviour
{
    // Set from inspector
    [SerializeField] private Character _mob;

    // Set from here
    private Node _root = null;

    void Start()
    {
        
        _root = SetupTree();
    }

    void Update()
    {
        if (_root != null && !_mob.IsDead) {
            _root.Evaluate();
        }
    }

    protected abstract Node SetupTree();
}
