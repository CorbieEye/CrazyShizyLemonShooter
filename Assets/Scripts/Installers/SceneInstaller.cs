using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    // Set from inspector
    [Header("Important")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Player character initialization")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _playerCharacterModel;

    [Header("CinemachineFreeLook")]
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

    [Header("UI")]
    [SerializeField] private HUD _hud;
    [SerializeField] private InventoryMenu _inventoryMenu;
    [SerializeField] private GameOverScreen _gameOverScreen;


    // Set from here
    GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public override void InstallBindings()
    {
        AddPlayerInput();
        AddCinemachineFreeLook();
        AddHud();
        AddInventoryMenu();
        CreatePlayerCharacter();
        AddGameOverScreen();
    }

    #region AddInventoryMenu
    private void AddInventoryMenu()
    {
        Container
            .Bind<InventoryMenu>()
            .FromInstance(_inventoryMenu)
            .AsSingle();

    }
    #endregion

    #region AddHud
    private void AddHud()
    {
        Container
            .Bind<HUD>()
            .FromInstance(_hud)
            .AsSingle();
    }
    #endregion

    #region AddPlayerInput
    private void AddPlayerInput() {
        Container
            .Bind<PlayerInput>()
            .FromInstance(_playerInput)
            .AsSingle()
            .NonLazy();
    }
    #endregion

    #region AddCinemachineFreeLook
    private void AddCinemachineFreeLook() {
        Container
            .Bind<CinemachineFreeLook>()
            .FromInstance(_cinemachineFreeLook)
            .AsSingle()
            .NonLazy();
    }
    #endregion

    #region CreatePlayerCharacter
    private void CreatePlayerCharacter() {
        GameObject go = Container.InstantiatePrefab(_playerCharacterModel, _startPoint.position, Quaternion.identity, null);
        PlayerCharacterController playerCharacterController = go.GetComponent<PlayerCharacterController>();

        Container
            .Bind<PlayerCharacterController>()
            .FromInstance(playerCharacterController)
            .AsSingle()
            .NonLazy();
    }
    #endregion

    #region AddGameOverScreen
    private void AddGameOverScreen() {
        Container
            .Bind<GameOverScreen>()
            .FromInstance(_gameOverScreen)
            .AsSingle();
    }
    #endregion
}
