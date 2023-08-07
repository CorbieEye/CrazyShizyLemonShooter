using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character stats", menuName = "Crazy shizy lemon shooter/Character stats")]
public class CharacterStatsSO : ScriptableObject
{
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _maxStamina;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _maxWeapons;

    private float MaxHealth { get { return _maxHealth; } }
    private float MaxStamina { get { return _maxStamina; } }
    private float RunSpeed { get { return _runSpeed; } }
    private float Damage { get { return _damage; } }
    private int MaxWeapons { get { return _maxWeapons; } }
}
