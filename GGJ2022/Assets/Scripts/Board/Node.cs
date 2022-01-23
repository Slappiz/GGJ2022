using Game;
using UnityEngine;

namespace Board
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public NodeType Type { get; private set; }
        public int Cost { get; private set; }
        public Team Team { get; private set; }
        public int Resoruces { get; private set; }
        public void Setup(NodeBlueprint blueprint)
        {
            Type = blueprint.Type;
            Cost = blueprint.ResourceCost;
            Resoruces = blueprint.ResourceTurn;
            
            _spriteRenderer.sprite = blueprint.Icon;
        }

        public void SetOwner(Team team)
        {
            Team = team;
        }
    }
}