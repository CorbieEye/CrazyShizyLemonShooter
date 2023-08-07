using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuGameState : GameState
{
    public MainMenuGameState(DiContainer diContainer = null) : base(diContainer) { 
        //
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnEnterState()
    {
        //
    }

    public override void OnQuitState()
    {
        //
    }
    public override void OnPressInventoryButton()
    {
        // Ничего не надо делать
    }
}
