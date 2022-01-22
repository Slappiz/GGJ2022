﻿using System;
using UnityEngine;

namespace Board
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private int _height = 5;
        [SerializeField] private int _width = 9;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private float _spacing = 2f;
        
        private Node[] _nodes;

        private void Awake()
        {
            _nodes = new Node[_height * _width];
            Build();
            
            // Set camera position
            // var camPos = Camera.main.gameObject.transform.position;
            // var newCamPos = new Vector3(_width * 0.5f * _spacing, _height * 0.5f * _spacing, camPos.z);
            // Camera.main.gameObject.transform.position = newCamPos;
        }

        private void Build()
        {
            for (int y = 0, i = 0; y < _height; y++) 
            {
                for (int x = 0; x < _width; x++) 
                {
                    CreateNode(x, y, i++);
                }
            }
        }

        private void CreateNode(int x, int y, int i)
        {
            Vector3 position;
            position.x = x * _spacing;
            position.y = y * _spacing;
            position.z = 0f;

            Node node = _nodes[i] = Instantiate(_nodePrefab);
            node.transform.SetParent(transform, false);
            node.transform.localPosition = position;
        }
    }
}