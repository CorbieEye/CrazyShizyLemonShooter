using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterWeaponMap", menuName = "Crazy shizy lemon shooter/CharacterWeaponMap")]
public class CharacterWeaponMap : ScriptableObject
{
    //
    private CharacterRangeWeaponMapping[] _rangeWeaponsMappings;

    public CharacterRangeWeaponMapping[] RangeWeaponsMappings { get { return _rangeWeaponsMappings; } }
}
