using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WonGameState : GameState
{
    private GameManager _gameManager;
    private GameOverScreen _gameOverScreen;

    public WonGameState(DiContainer diContainer) : base (diContainer) {
        //
        _gameManager = diContainer.Resolve<GameManager>();
        _gameOverScreen = diContainer.Resolve<GameOverScreen>();
    }

    //
    public override void OnEnterState()
    {
        //
        _gameManager.IncrementWinsCount();
        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.ShowResults(true, _gameManager.GetWinsCount(), _gameManager.GetLossesCount());
    }

    public override void OnFixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnPressInventoryButton()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnQuitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
