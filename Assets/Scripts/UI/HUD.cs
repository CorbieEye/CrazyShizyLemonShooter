using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUD : MonoBehaviour
{
    // Set from inspector
    [SerializeField] private Image _hpBar;

    // Set from public setters
    private Character _playerCharacter;

    public void SetPlayerCharacter(Character playerCharacter) {
        _playerCharacter = playerCharacter;
    }

    public void UpdateHud() {
        UpdateHpBar();
    }

    #region UpdateHpBar
    private void UpdateHpBar() {
        if (_playerCharacter.CurrentHealth > 0)
        {
            _hpBar.transform.localScale =
                new Vector3(
                _playerCharacter.CurrentHealth / _playerCharacter.MaxHealth
                , _hpBar.transform.localScale.y
                , _hpBar.transform.localScale.z
                );
        }
        else
        {
            _hpBar.transform.localScale =
                new Vector3(0, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
        }
    }
    #endregion
}
