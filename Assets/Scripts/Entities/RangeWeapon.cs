using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    //
    private RangeWeaponSO _rangeWeaponSO;
    public RangeWeaponSO RangeWeaponSO { get { return _rangeWeaponSO; } }

    public RangeWeapon(RangeWeaponSO rangeWeaponSO) : base(rangeWeaponSO) {
        _rangeWeaponSO = rangeWeaponSO;
    }

    public override GameObject Model {
        get {
            return _rangeWeaponSO.Model;
        }
    }
}
