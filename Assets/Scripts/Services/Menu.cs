using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Menu : MonoBehaviour
{
    //
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager) {
        _gameManager = gameManager;
    }
}
