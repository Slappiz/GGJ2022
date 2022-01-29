using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board
{
    public class Board : MonoBehaviour
    {
        [Header("Blueprints")] 
        [SerializeField] private NodeBlueprint _defaultNode;
        [SerializeField] private NodeBlueprint _rootNode;
        [SerializeField] private NodeBlueprint _resourceNode;
        [SerializeField] private NodeBlueprint _fortNode;
        [SerializeField] private NodeBlueprint _trapNode;
        
        [Header("Board Settings")]
        [SerializeField] private int _height = 5;
        [SerializeField] private int _width = 9;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private float _spacing = 2f;
        [SerializeField] private int _minNodeCount = 2;
        [SerializeField] private int _maxNodeCount = 3;

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
                    yield return new WaitForSeconds(0.1f);
                    n.SetColor(Color.blue);
                }
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
                currentPathNode.SetColor(Color.green);
                path.Add(currentPathNode);
                currentPathNode = cameFrom[currentPathNode];
                yield return new WaitForSeconds(0.1f);
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
                    case NodeType.Resource:
                        node.Setup(_resourceNode);
                        break;
                    case NodeType.Fort:
                        node.Setup(_fortNode);
                        break;
                    case NodeType.Trap:
                        node.Setup(_trapNode);
                        break;
                    case NodeType.None:
                    case NodeType.Root:
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
            foreach (var neighbor in StartNode.Neighbors)
            {
                if (neighbor == null) continue;
                // Set all neighbors to default
                neighbor.Setup(_defaultNode);
            }
      
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