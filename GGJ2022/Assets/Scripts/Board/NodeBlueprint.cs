using UnityEngine;

namespace Board
{
    public class NodeBlueprint : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private NodeType _type;
        [SerializeField] private int _resourceCost;
        [SerializeField] private int _resourceTurn;
        public Sprite Icon => _icon;
        public NodeType Type => _type;
        public int ResourceCost => _resourceCost;
        public int ResourceTurn => _resourceTurn;
    }
}
