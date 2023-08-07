using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using Zenject;

public class Character : MonoBehaviour, IDamageReceiver, IDamageSource
{
    // Set from inspector
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private List<ConsumableItemSO> _testItems;
    [SerializeField] private RangeWeaponSO _testWeapon;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private float _fistPunchDistance;
    [SerializeField] private float _fistPunchAngle;
    [SerializeField] private AnimationClip _defaultIdleAnimation, _defaultPunchAnimation;
    [SerializeField] private AnimatorController _originalAnimatorController;

    // Set from here
    private Animator _animator;
    private float _currentHealth;
    private Rigidbody[] _bodyPartsRigidbodies;
    private CapsuleCollider[] _bodyPartsCapsuleColliders;
    private SphereCollider[] _bodyPartsSphereColliders;
    private BoxCollider[] _bodyPartsBoxColliders;
    private HUD _hud;
    private Inventory _inventory;
    private Weapon _currentWeapon;
    [SerializeField]private GameObject _currentWeaponModel;
    private DiContainer _diContainer;
    GameManager _gameManager;

    // Public
    public Inventory Inventory { get { return _inventory; } }
    public Weapon CurrentWeapon { get { return _currentWeapon; } }
    public GameObject CurrentWeaponModel { get { return _currentWeaponModel; } }



    [Inject]
    public void Construct(HUD hud, DiContainer diContainer) {
        // Если это персонаж игрока, то привзяываем hud к этому персонажу
        if (IsPlayer)
        {
            _hud = hud;
            _hud.SetPlayerCharacter(this);
        }

        _diContainer = diContainer;
        _gameManager = diContainer.Resolve<GameManager>();
    }

    public bool IsPlayer { get {return GetComponent<PlayerCharacterController>() != null;} }

    void Start() {
        _inventory = GetComponent<Inventory>();
        foreach (var itemSO in _testItems)
        {
            _inventory.Additem(new ConsumableItem(itemSO, 5), 5);
        }
        if (_testWeapon != null)
        {
            _inventory.Additem(new RangeWeapon(_testWeapon));
        }
    }

    // Public
    public bool IsDead { get {
            bool isDead = _currentHealth <= 0;
            return isDead; 
        } }
    public float CurrentHealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _characterStats.MaxHealth; } }

    private void Awake()
    {
        _currentHealth = _characterStats.MaxHealth;
        _animator = GetComponent<Animator>();
        SetupPhysics();
    }

    public float GetActualWalkSpeed() {
        return _characterStats.WalkSpeed;
    }

    public float GetActualRunSpeed() {
        return _characterStats.RunSpeed;
    }

    public void ReceiveDamage(DamageData damageData)
    {
        _currentHealth -= damageData.Value;
        if (_currentHealth <= 0) {
            Die();
        }
        if (IsPlayer)
        {
            _hud.UpdateHud();
        }
        else {
            var thisMobData = GetComponent<MobData>();
            var bb = Physics.OverlapSphere(gameObject.transform.position, thisMobData.DistanceToCallBros);
            var mobDatas = UnityEngine.Object.FindObjectsOfType(typeof(MobData)) as MobData[];
            foreach (var mobData in mobDatas) {
                if (Vector3.Distance(gameObject.transform.position, mobData.gameObject.transform.position) <= thisMobData.DistanceToCallBros
                    && mobData.GetComponent<MonsterBehaviourTree>() != null
                    ) 
                { 
                    Character aggressor = (Character)(damageData.Source);
                    if (aggressor != null) {
                        mobData.Aggressor = aggressor;
                    }
                }
            }
        }
    }

    private void Die() {
        SetRagdollActive(true);
        _animator.enabled = false;
        var navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null) {
            navMeshAgent.ResetPath();
        }

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        var playerCharacterController = GetComponent<CharacterController>();
        if (playerCharacterController != null)
        {
            _gameManager.SetState(new LostGameState(_diContainer));
        }

        else {
            MonsterBehaviourTree[] monsterBehaviourTrees = UnityEngine.Object.FindObjectsOfType(typeof(MonsterBehaviourTree)) as MonsterBehaviourTree[];
            Character[] mobs = monsterBehaviourTrees.Select(x => x.GetComponent<Character>()).ToArray();
            Character[] deadMobs = mobs.Where(x => x.IsDead).ToArray();
            if (mobs.Count() == deadMobs.Count()) {
                _gameManager.SetState(new WonGameState(_diContainer));
            }
        }
    }

    public void Heal(float value) {
        if (IsDead) {
            return;
        }

        if (_currentHealth + value > _characterStats.MaxHealth)
        {
            _currentHealth = _characterStats.MaxHealth;
        }
        else {
            _currentHealth += value;
        }
        if (IsPlayer)
        {
            _hud.UpdateHud();
        }
    }

    #region SetupPhysics
    private void SetupPhysics() {
        _bodyPartsRigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollActive(false);
        _bodyPartsCapsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        _bodyPartsSphereColliders = GetComponentsInChildren<SphereCollider>();
        _bodyPartsBoxColliders = GetComponentsInChildren<BoxCollider>();
    }
    #endregion

    private void SetRagdollActive(bool state) {
        var characterController = GetComponent<CharacterController>();
        if (state && characterController != null) {
            characterController.enabled = false;
        }
        foreach (var rigidbody in _bodyPartsRigidbodies)
        {
            rigidbody.isKinematic = !state;
        }
    }

    public void EquipWeapon(Weapon weapon) {
        var animator = GetComponent<Animator>();

        
        if (_currentWeapon == weapon) {
            Destroy(_currentWeaponModel);
            _currentWeapon = null;
            animator.SetLayerWeight(1, 0f);
            animator.runtimeAnimatorController = _originalAnimatorController;
            return;
        }

        if (_currentWeaponModel != null) {
            Destroy(_currentWeaponModel);
        }

        _currentWeapon = weapon;
        _currentWeaponModel = Instantiate(_currentWeapon.Model) as GameObject;
        _currentWeaponModel.transform.SetParent(_rightHand.transform);
        _currentWeaponModel.transform.localPosition = Vector3.zero;
        _currentWeaponModel.transform.localRotation = Quaternion.Euler(
                weapon.WeaponSO.Rotation.x
                , weapon.WeaponSO.Rotation.y
                , weapon.WeaponSO.Rotation.z
            );
        var collider = _currentWeaponModel.GetComponent<Collider>();
        if (collider != null) {
            collider.enabled = false;
        }

        var newAnimatorController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        newAnimatorController[_defaultIdleAnimation] = weapon.WeaponSO.IdleClip;
        newAnimatorController[_defaultPunchAnimation] = weapon.WeaponSO.AttackClip;
        animator.SetLayerWeight(1, 1f);
        animator.runtimeAnimatorController = newAnimatorController;
        
    }

    public void FistPunch()
    {
        //
        Debug.Log("FistPunch");
        Collider[] colliders = Physics.OverlapSphere(transform.position, _fistPunchDistance);
        var damageReceivers = new List<IDamageReceiver>();
        foreach (var collider in colliders) {
            var root = collider.transform.root;
            var damageReceiver = root.gameObject.GetComponent<IDamageReceiver>();
            if (damageReceiver != null) {
                damageReceivers.Add(damageReceiver);
            }
        }
        damageReceivers = damageReceivers.Distinct().ToList();
        if (damageReceivers.Contains(this)) {
            damageReceivers.Remove(this);
        }

        foreach (var damageReceiver in damageReceivers) {
            Component damageReceiverAsComponent = (Component)damageReceiver;
            Quaternion look = Quaternion.LookRotation(damageReceiverAsComponent.transform.position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, look);

            if (angle <= _fistPunchAngle) {
                damageReceiver.ReceiveDamage(new DamageData { Value = _characterStats.FistDamage, Source = this});
            }
        }

        _animator.SetBool("Shooting", false);
    }
}
