using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryGameState : GameState
{
    private InventoryMenu _inventoryMenu;
    private CinemachineFreeLook _cinemachineFreeLook;

    public InventoryGameState(DiContainer diContainer) : base (diContainer) {
        _inventoryMenu = diContainer.Resolve<InventoryMenu>();
        _cinemachineFreeLook = diContainer.Resolve<CinemachineFreeLook>();
    }

    public override void OnEnterState()
    {
        _inventoryMenu.gameObject.SetActive(true);
        _cinemachineFreeLook.enabled = false;
        UnityEngine.Time.timeScale = 0;
        _inventoryMenu.UpdateInventoryMenu();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnPressInventoryButton()
    {
        _gameManager.SetState(new PlayingGameState(_diContainer));
    }

    public override void OnQuitState()
    {
        _inventoryMenu.gameObject.SetActive(false);
        UnityEngine.Time.timeScale = 1;
        _cinemachineFreeLook.enabled = true;
    }

    public override void OnUpdate()
    {
        //
    }
}
