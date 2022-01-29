using Game;
using UnityEngine;

namespace Board
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _hoverHighlight;
        [SerializeField] private GameObject _selectedHighlight;
        [SerializeField] private Node[] _neighbors = new Node[8];

        public Coordinates Coordinate { get; set; }
        public NodeType Type { get; private set; }
        public int Cost { get; private set; }
        public Team Team { get; private set; }
        public int Resources { get; private set; }
        public Node[] Neighbors => _neighbors;
        public bool Active => gameObject.activeSelf;

        public void Setup(NodeBlueprint blueprint)
        {
            Type = blueprint.Type;
            Cost = blueprint.ResourceCost;
            Resources = blueprint.ResourceTurn;
            
            _spriteRenderer.sprite = blueprint.Icon;
            _spriteRenderer.color = blueprint.Color;
            
            SetSelectedHighlight(false);
            SetHoverHighlight(false);
            
            ToggleNode(true);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
        
        public void SetHoverHighlight(bool active)
        {
            _hoverHighlight.SetActive(active);
        }

        public void SetSelectedHighlight(bool active)
        {
            _selectedHighlight.SetActive(active);
        }
        
        public Node GetNeighbor(NodeDirection direction)
        {
            return _neighbors[(int) direction];
        }
        
        public void SetNeighbor(NodeDirection direction, Node node)
        {
            _neighbors[(int) direction] = node;
            node._neighbors[(int) direction.Opposite()] = this;
        }
        
        public Node GetFirstDiagonalNeighbor(NodeDirection direction)
        {
            switch (direction)
            {
                case NodeDirection.NW:
                case NodeDirection.N:
                    return _neighbors[7];
                case NodeDirection.E:
                case NodeDirection.NE:
                    return _neighbors[1];
                case NodeDirection.SE:
                case NodeDirection.S:
                    return _neighbors[3];
                case NodeDirection.SW:
                case NodeDirection.W:
                    return _neighbors[5];
                default:
                    return _neighbors[(int)direction];
            }
        }
        
        public Node GetSecondDiagonalNeighbor(NodeDirection direction)
        {
            switch (direction)
            {
                case NodeDirection.W:
                case NodeDirection.NW:
                    return _neighbors[7];
                case NodeDirection.N:
                case NodeDirection.NE:
                    return _neighbors[1];
                case NodeDirection.E:
                case NodeDirection.SE:
                    return _neighbors[3];
                case NodeDirection.S:
                case NodeDirection.SW:
                    return _neighbors[5];
                default:
                    return _neighbors[(int)direction];
            }
        }
        
        public void ToggleNode(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetOwner(Team team)
        {
            Team = team;
        }
    }
}