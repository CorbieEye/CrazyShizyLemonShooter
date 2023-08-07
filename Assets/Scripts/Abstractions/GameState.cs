using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public abstract class GameState
{
    protected DiContainer _diContainer;
    protected GameManager _gameManager;
    public GameState(DiContainer diContainer = null) {
        if (diContainer != null)
        {
            _diContainer = diContainer;
            _gameManager = diContainer.Resolve<GameManager>();
        }
    }
    public abstract void OnEnterState();
    public abstract void OnQuitState();

    public abstract void OnUpdate();

    public abstract void OnFixedUpdate();
    public abstract void OnPressInventoryButton();
}
