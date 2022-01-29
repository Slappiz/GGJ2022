using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Board
{
    public class Board : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private Canvas _boardCanvas;
        [SerializeField] private Text _nodeLabelPrefab;
        
        [Header("Blueprints")] 
        [SerializeField] private NodeBlueprint _defaultNode;
        [SerializeField] private NodeBlueprint _rootNode;
        [SerializeField] private NodeBlueprint _trapNode;
        
        [Header("Board Settings")]
        [SerializeField] private int _height = 5;
        [SerializeField] private int _width = 9;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private float _spacing = 2f;

        private Coroutine _activeCoroutine;
        private bool _boardIsValid;
        
        public Node[] Nodes { get; private set; }
        public Node StartNode { get; private set; }
        public Node GoalNode { get; private set; }

        public IEnumerator Build()
        {
            Nodes = new Node[_height * _width];
            
            for (int y = 0, i = 0; y < _height; y++) 
            {
                for (int x = 0; x < _width; x++) 
                {
                    CreateNode(x, y, i++);
                }
            }

            yield return SetupBoard();
        }

        private IEnumerator SetupBoard()
        {
            while (!_boardIsValid)
            {
                RandomizeBoard();
                yield return BreadthFirstSearch();
            }
            
            GoalNode.Reveal();
            GoalNode.SetColor(Color.green);
            
            StartNode.SetOwner(Team.Player);
            foreach (var neighbor in StartNode.Neighbors)
            {
                if (neighbor == null) continue;
                // Set all neighbors to default
                neighbor.Setup(_defaultNode);
            }
            EvaluateNodes();
        }

        public void EvaluateNodes()
        {
            foreach (var node in Nodes)
            {
                node.Label.text = string.Empty;
                var trapCount = 0;
                foreach (var neighbor in node.Neighbors)
                {
                    if(neighbor == null) continue;
                    if (neighbor.Type == NodeType.Trap)
                    {
                        trapCount++;
                    }
                }

                node.Label.text = trapCount.ToString();
            }
        }

        private IEnumerator BreadthFirstSearch()
        {
            var frontier = new Queue<Node>();
            frontier.Enqueue(StartNode);

            var cameFrom = new Dictionary<Node,Node>();
            cameFrom.Add(StartNode, StartNode);
            
            // Frontier
            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                foreach (var n in current.Neighbors)
                {
                    if(n == null || n.Type == NodeType.Trap) continue;
                    if(cameFrom.ContainsKey(n)) continue;
                    frontier.Enqueue(n);
                    cameFrom.Add(n, current);
                }
                yield return null;
            }

            if (!cameFrom.ContainsKey(GoalNode))
            {
                _boardIsValid = false;
                yield break;
            }
            
            // path
            var currentPathNode = GoalNode; // goal
            var path = new List<Node>();
            while (currentPathNode != StartNode)
            {
                path.Add(currentPathNode);
                currentPathNode = cameFrom[currentPathNode];
                yield return null;
            }

            _boardIsValid = true;
        }
        

        private Node GetNodeFromCoordinates(int x, int y)
        {
            return Nodes.FirstOrDefault(node => node.Coordinate.X == x && node.Coordinate.Y == y);
        }

        private void RandomizeBoard()
        {
            foreach (var node in Nodes)
            {
                var typeIndex = Random.Range(2, Enum.GetNames(typeof(NodeType)).Length); // Skip NodeType 'None' and 'Root'
                var type = (NodeType)typeIndex;

                switch (type)
                {
                    case NodeType.Standard:
                        node.Setup(_defaultNode);
                        break;
                    case NodeType.Trap:
                        node.Setup(_trapNode);
                        break;
                    default:
                        Debug.Log("Shouldn't get here!");
                        break;
                }
            }
            
            // Set start
            var firstColumnNodes = GetNodesFromColumn(0);
            var rootIndex = Random.Range(0, _height);
            firstColumnNodes[rootIndex].Setup(_rootNode);
            StartNode = firstColumnNodes[rootIndex];

            // Set Goal
            var lastColumnNodes = GetNodesFromColumn(_width - 1);
            var goalIndex = Random.Range(0, _height);
            lastColumnNodes[goalIndex].Setup(_rootNode);
            GoalNode = lastColumnNodes[goalIndex];
        }

        private List<Node> GetNodesFromColumn(int x)
        {
            return Nodes.Where(node => node.Coordinate.X == x).ToList();
        }
        
        private void CreateNode(int x, int y, int i)
        {
            Vector3 position;
            position.x = x * _spacing;
            position.y = y * _spacing;
            position.z = 0f;

            var node = Nodes[i] = Instantiate(_nodePrefab);
            node.name = $"Node {i} - ({x},{y})";
            Transform transform1;
            (transform1 = node.transform).SetParent(transform, false);
            transform1.localPosition = position;
            node.Coordinate = Coordinates.FromOffsetCoordinates(x, y);

            var label = Instantiate(_nodeLabelPrefab);
            label.rectTransform.SetParent(_boardCanvas.transform,false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            node.Label = label;
            node.Label.enabled = false;
            
            // Set neighbors
            if (x > 0 && x < _width)
            {
                node.SetNeighbor(NodeDirection.W, Nodes[i - 1]);
            }

            if (y <= 0) return;
            
            node.SetNeighbor(NodeDirection.S, Nodes[i - _width]);
            if (x > 0 && x < _width)
            {
                node.SetNeighbor(NodeDirection.SW, Nodes[i - _width - 1]);
            }

            if (x < _width - 1)
            {
                node.SetNeighbor(NodeDirection.SE, Nodes[i - _width + 1]);
            }
        }
    }
}