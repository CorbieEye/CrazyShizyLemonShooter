using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Crazy shizy lemon shooter/Effect")]
public class EffectSO : ScriptableObject
{
    [SerializeField]
    private float _healthAddition;

    public void Consume(Character user)
    {
        user.Heal(_healthAddition);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (_healthAddition > 0) {
            sb.Append("Восстанавливает ");
            sb.Append(_healthAddition);
            sb.Append(" здоровья\n");
        }
        else if (_healthAddition < 0) {
            sb.Append("Отнимает ");
            sb.Append(_healthAddition);
            sb.Append(" здоровья\n");
        }
        return sb.ToString();
    }
}
