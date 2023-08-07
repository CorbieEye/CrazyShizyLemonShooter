using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged weapon", menuName = "Crazy shizy lemon shooter/Ranged weapon")]
public class RangeWeaponSO : WeaponSO
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private int _cartridgesIntTheClip;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _shotLength;
    [SerializeField]
    private bool _hasOpticalSight;
    [SerializeField]
    private Sprite _opticalSightSprite;
    [SerializeField]
    private AudioClip _shootingSound;
    [SerializeField]
    private AudioClip _reloadingSound;


    public GameObject BulletPrefab { get { return _bulletPrefab; } }
    public Vector3 Offset { get { return _offset; } }
    public int CartridgesIntTheClip { get { return _cartridgesIntTheClip; } }
    public float Damage { get { return _damage; } }
    public float ShotLength { get { return _shotLength; } }
    public bool HasOpticalSight { get { return _hasOpticalSight; } }
    public Sprite OpticalSightSprite { get { return _opticalSightSprite; } }
    public AudioClip ShootingSound { get { return _shootingSound; } }
    public AudioClip ReloadingSound { get { return _reloadingSound; } }

    public override Item ToItem()
    {
        var rangeWeapon = new RangeWeapon(this);
        return rangeWeapon;
    }
}
