using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ItemSO
{
    [SerializeField]
    private Vector3 _rotation;
    [SerializeField]
    private AnimationClip _attackClip, _idleClip;

    public Vector3 Rotation { get { return _rotation; } }

    public override bool IsStackable { get { return false; } }
    public AnimationClip AttackClip {get { return _attackClip; } }
    public AnimationClip IdleClip {get { return _idleClip; } }
}
