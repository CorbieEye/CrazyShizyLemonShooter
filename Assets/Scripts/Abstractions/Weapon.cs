using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    private WeaponSO _weaponSO;
    public WeaponSO WeaponSO { get { return _weaponSO; } }

    public Weapon(WeaponSO weaponSO) : base(weaponSO, 1) {
        _weaponSO = weaponSO;
    }
    public abstract GameObject Model { get; }

    public override void Use(Character user)
    {
        user.EquipWeapon(this);
    }
}
