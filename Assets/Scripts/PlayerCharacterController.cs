using Cinemachine;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerCharacterController : MonoBehaviour
{
    // Назначаются в инспекторе
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private LayerMask _aimColliderMask = new LayerMask();
    [SerializeField] private Transform _aimTarget;

    // Назначаются из этого класса
    [SerializeField] Character _character;
    Vector2 _inputVector2 = Vector2.zero;
    float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;
    Vector3 _velocity;

    Transform _cam;
    PlayerInput _playerInput;
    GameManager _gameManager;
    CinemachineFreeLook _cinemachineFreeLook;
    DiContainer _diContainer;
    Vector3 _mouseWorldPosition;
    bool _aiming;
    bool _runs;

    // Public
    public Animator Animator { get { return _animator; } }
    public PlacebleItem PlacebleItem;

    // Константы
    readonly float _gravity = -9.81f;

    [Inject]
    public void Construct(CinemachineFreeLook cinemachineFreeLook, PlayerInput playerInput, DiContainer diContainer, HUD hud) {
        _cinemachineFreeLook = cinemachineFreeLook;
        _playerInput = playerInput;
        _gameManager = diContainer.Resolve<GameManager>();
        _diContainer = diContainer;
    }

    void Start()
    {
        _cinemachineFreeLook.Follow = _cameraPivot;
        _cinemachineFreeLook.LookAt = _cameraPivot;
        _cam = Camera.main.transform;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().started += MovementPerformed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().performed += MovementPerformed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().canceled += MovementPerformed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().started += Jump;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().performed += Jump;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().canceled += Jump;
        _gameManager.SetState(new PlayingGameState(_diContainer));
        _character = GetComponent<Character>();
    }

    public void HandleCharacterControlOnUpdate() {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderMask)) {
            _mouseWorldPosition = raycastHit.point;
            _aimTarget.position = raycastHit.point;
        }

        if (!_character.IsDead)
        {
            var rightHandIkConstraint = GetComponentInChildren<ChainIKConstraint>();

            // Поворачиваем перса к цели
            Vector3 worldAimTarget = _mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            var aimDir = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }

        // Падение
    }

    public void HandleCharacterControlOnFixedUpdate()
    {
        if (_characterController.isGrounded) {
            _velocity = Vector3.zero;
            
        }

        // Передвижение
        if (!_character.IsDead)
        {
            Vector3 direction = new Vector3(_inputVector2.x, 0, _inputVector2.y).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0f, angle, 0);

                //Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Vector3 moveDirection = transform.right * _inputVector2.x + _inputVector2.y * transform.forward;
                _characterController.Move(moveDirection * (_runs ? _character.GetActualRunSpeed() : _character.GetActualWalkSpeed()) * Time.deltaTime);
            }
        }

        // Падение
        _velocity.y += (_gravity) * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
        

        //Анимации
        if (!_character.IsDead)
        {
            if (_inputVector2 != Vector2.zero)
            {
                _animator.SetBool("Move", true);
                if (_runs)
                {
                    _animator.SetFloat("MoveSpeed", _character.GetActualRunSpeed() / _character.GetActualWalkSpeed());
                }
                else {
                    _animator.SetFloat("MoveSpeed", 1);
                }
            }
            else
            {
                _animator.SetBool("Move", false);
            }
        }
    }

    #region Инпуты пользователя
    public void MovementPerformed(InputAction.CallbackContext context) {
        _inputVector2 = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) {
        //_character.ReceiveDamage(new DamageData { Value = 3, Source = _character });

    }

    public void AttackButtonPressed(InputAction.CallbackContext context) {
        if (!context.canceled)
        {
            _animator.SetBool("Shooting", true);
        }
    }
    public void AimButtonPressed(InputAction.CallbackContext context) {
        //
    }

    public void ActivateButtonPressed(InputAction.CallbackContext context) {
        if (context.performed && PlacebleItem != null) {
            PlacebleItem.TryPick();
        }
    }

    public void RunButtonPressed(InputAction.CallbackContext context) {
        if (context.started) {
            _runs = true;
        }
        if (context.canceled) {
            _runs = false;
        }
    }
    #endregion

    public void CreateAndLaunchTheBullet()
    {
        if (!_character.IsDead && _character.CurrentWeapon is RangeWeapon)
        {
            if (_character.CurrentWeapon is RangeWeapon)
            {
                Vector3 worldAimTarget = _mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                var aimDir = (worldAimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 60f);
            }

            var spawnBulletPosition = _character.CurrentWeaponModel.GetComponentInChildren<BulletSource>().gameObject.transform.position;
            var bullet = Instantiate(((RangeWeaponSO)_character.CurrentWeapon.WeaponSO).BulletPrefab, spawnBulletPosition, Quaternion.identity, null) as GameObject;
            bullet.transform.LookAt(_mouseWorldPosition);
            var DamageData = new DamageData { Source = _character, Value = 5 };
            bullet.GetComponent<BulletProjectile>().DamageData = DamageData;
            _animator.SetBool("Shooting", false);
        }
    }

    private void Destroy()
    {
        // Отписываемся от событий инпута пользователя
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().started -= MovementPerformed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().performed -= MovementPerformed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Movement").FirstOrDefault().canceled -= MovementPerformed;

        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().started -= Jump;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().performed -= Jump;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Jumping").FirstOrDefault().canceled -= Jump;
    }

    private void TryPickItem() { 
        
    }
}
