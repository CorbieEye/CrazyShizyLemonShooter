using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterRangeWeaponMapping
{
    public readonly CharacterSO Character;
    public readonly RangeWeaponSO Weapon;
    public readonly AnimationClip IdleAnimation, ReloadAnimation, ShootingAnimation;
    public readonly Vector3 Offset;
}
