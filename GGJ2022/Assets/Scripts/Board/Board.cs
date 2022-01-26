using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Board
{
    public class Board : MonoBehaviour
    {
        [Header("Blueprints")] 
        [SerializeField] private NodeBlueprint _defaultNode;
        [SerializeField] private NodeBlueprint _rootNode;
        [SerializeField] private NodeBlueprint _resourceNode;
        [SerializeField] private NodeBlueprint _fortNode;
        
        [Header("Board Settings")]
        [SerializeField] private int _height = 5;
        [SerializeField] private int _width = 9;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private float _spacing = 2f;
        [SerializeField] private int _minNodeCount = 2;
        [SerializeField] private int _maxNodeCount = 3;
        
        public Node[] Nodes { get; private set; }
        
        // private void Awake()
        // {
        //     _nodes = new Node[_height * _width];
        //     Build();
        //     
        //     // Set camera position
        //     // var camPos = Camera.main.gameObject.transform.position;
        //     // var newCamPos = new Vector3(_width * 0.5f * _spacing, _height * 0.5f * _spacing, camPos.z);
        //     // Camera.main.gameObject.transform.position = newCamPos;
        // }

        public void Build()
        {
            Nodes = new Node[_height * _width];
            
            for (int y = 0, i = 0; y < _height; y++) 
            {
                for (int x = 0; x < _width; x++) 
                {
                    CreateNode(x, y, i++);
                }
            }
            
            SetupNodes();
        }

        private void SetupNodes()
        {
            for (var x = 0; x < _width;x++)
            {
                var nodes = GetNodesFromColumn(x);

                if (x == 0 || x == _width - 1)
                {
                    var rootIndex = Random.Range(0, _height);
                    for (var i = 0; i < nodes.Count; i++)
                    {
                        if (rootIndex == i)
                        {
                            nodes[i].Setup(_rootNode);
                            continue;
                        }
                        nodes[i].ToggleNode(false);
                    }
                }
                else
                {
                    var resIndex = Random.Range(0, _height);
                    for (var i = 0; i < nodes.Count; i++)
                    {
                        if (resIndex == i)
                        {
                            nodes[i].Setup(_resourceNode);
                            continue;
                        }
                        nodes[i].ToggleNode(false);
                    }
                }
            }
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
            node.transform.SetParent(transform, false);
            node.transform.localPosition = position;
            node.Coordinate = Coordinates.FromOffsetCoordinates(x, y);
            // Set neighbors
            if (x > 0)
            {
                node.SetNeighbor(NodeDirection.W, Nodes[i - 1]);
            }
            if (y > 0)
            {
                node.SetNeighbor(NodeDirection.S, Nodes[i - _width]);
                if (x > 0)
                {
                    node.SetNeighbor(NodeDirection.SW, Nodes[i - _width - 1]);
                }

                if (x < _width)
                {
                    node.SetNeighbor(NodeDirection.SE, Nodes[i - _width + 1]);
                }
            }
        }
    }
}