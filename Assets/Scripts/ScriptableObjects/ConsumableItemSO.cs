using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable item", menuName = "Crazy shizy lemon shooter/Consumable item")]
public class ConsumableItemSO : ItemSO
{
    [SerializeField]
    private EffectSO[] _effects;
    public override bool IsStackable { get { return true; } }

    public EffectSO[] Effects { get { return _effects; } }

    public override Item ToItem()
    {
        var consumable = new ConsumableItem(this, 1);
        return consumable;
    }

    public override string ToString () {
        StringBuilder sb = new StringBuilder();

        foreach (var e in _effects) {
            sb.Append(e.ToString());
            sb.Append("\n");
        }

        return sb.ToString();
    }
}
