using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text _winsCountText, _lossesCountText;
    [SerializeField] private Text _resultText;
    [SerializeField] private Button _toMainMenuButton;
    [SerializeField] private HUD _hud;

    public void ShowResults(bool won, int winsCount, int lossesCount) {
        if (won)
        {
            _resultText.text = "Победа!!!";
            _resultText.color = Color.green;
        }
        else {
            _resultText.text = "Поражение...";
            _resultText.color = Color.red;
        }
        _winsCountText.text = winsCount.ToString();
        _lossesCountText.text = lossesCount.ToString();
        _toMainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
        _hud.gameObject.SetActive(false);
    }
}
