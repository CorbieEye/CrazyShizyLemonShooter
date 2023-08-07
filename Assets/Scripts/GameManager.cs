using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameManager : MonoBehaviour
{
    // Состояния
    private GameState _gameState;
    public GameState State { get { return _gameState; } }

    public void SetState(GameState state) {
        if (_gameState != null) {
            _gameState.OnQuitState();
        }
        _gameState = state;
        _gameState.OnEnterState();
    }

    void Start() {
        SetState(new MainMenuGameState());
    }

    void Update()
    {
        _gameState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _gameState.OnFixedUpdate();
    }

    public void InventoryButtonPressed(InputAction.CallbackContext context) {
        _gameState.OnPressInventoryButton();
    }

    public void StartGame() {
        Debug.Log("Start game");
    }

    public void QuitGame() {
        //
        Debug.Log("Quit game");
    }

    public int GetWinsCount() {
        if (!PlayerPrefs.HasKey("Wins"))
        {
            PlayerPrefs.SetInt("Wins", 0);
        }

        return PlayerPrefs.GetInt("Wins");
    }

    public int GetLossesCount() {
        if (!PlayerPrefs.HasKey("Losses")) {
            PlayerPrefs.SetInt("Losses", 0);
        }
        return PlayerPrefs.GetInt("Losses");
    }

    public void IncrementWinsCount() {
        PlayerPrefs.SetInt("Wins", GetWinsCount() + 1);
    }

    public void IncrementLossesCount() {
        PlayerPrefs.SetInt("Losses", GetWinsCount() + 1);
    }

    public void PrintSmth() {
        Debug.Log("smth");
    }
}
