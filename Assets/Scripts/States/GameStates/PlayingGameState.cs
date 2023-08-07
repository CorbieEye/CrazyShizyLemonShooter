using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayingGameState : GameState
{
    private PlayerCharacterController _playerCharacterController;
    private PlayerInput _playerInput;

    public PlayingGameState(DiContainer diContainer) : base(diContainer) {
        _playerCharacterController = diContainer.Resolve<PlayerCharacterController>();
        _playerInput = diContainer.Resolve<PlayerInput>();
    }

    public override void OnUpdate()
    {
        _playerCharacterController.HandleCharacterControlOnUpdate();
    }

    public override void OnFixedUpdate() {
        _playerCharacterController.HandleCharacterControlOnFixedUpdate();
    }

    public override void OnEnterState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Attack").FirstOrDefault().performed += _playerCharacterController.AttackButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "AimOrSpecialAttack").FirstOrDefault().performed += _playerCharacterController.AimButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "AimOrSpecialAttack").FirstOrDefault().canceled += _playerCharacterController.AimButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Activate").FirstOrDefault().performed += _playerCharacterController.ActivateButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Running").FirstOrDefault().started += _playerCharacterController.RunButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Running").FirstOrDefault().canceled += _playerCharacterController.RunButtonPressed;
    }

    public override void OnQuitState()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Attack").FirstOrDefault().performed -= _playerCharacterController.AttackButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "AimOrSpecialAttack").FirstOrDefault().performed -= _playerCharacterController.AimButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "AimOrSpecialAttack").FirstOrDefault().canceled -= _playerCharacterController.AimButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Activate").FirstOrDefault().performed -= _playerCharacterController.ActivateButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Running").FirstOrDefault().started -= _playerCharacterController.RunButtonPressed;
        _playerInput.currentActionMap.actions.Where(x => x.name == "Running").FirstOrDefault().canceled -= _playerCharacterController.RunButtonPressed;
    }

    public override void OnPressInventoryButton()
    {
        _gameManager.SetState(new InventoryGameState(_diContainer));
    }
}
