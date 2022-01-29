﻿using Board;
using UnityEngine;
using Variables;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NodeVariable SelectedNode; // Todo: create SO variable
        [SerializeField] private NodeVariable HoverNode; // Todo: create SO variable
        
        public bool Enabled { get; set; }
        
        void Update()
        {
            if (!Enabled) return;

            Hover();

            if (Input.GetMouseButtonDown(0))
            {
                SelectNode();
            }
        }

        private void Hover()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                var node = hit.collider.gameObject.GetComponent<Node>();
                if (HoverNode.RuntimeValue == node) return;
                if (HoverNode.RuntimeValue != null) { HoverNode.RuntimeValue.SetHoverHighlight(false); }
                HoverNode.RuntimeValue = node;
                HoverNode.RuntimeValue.SetHoverHighlight(true);
                //Debug.Log(node != null ? $"Hover {node.name}" : "No hover");
            }
        }
        
        private void SelectNode()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                var node = hit.collider.gameObject.GetComponent<Node>();
                if (node == null) return;
                if (SelectedNode.RuntimeValue != null) { SelectedNode.RuntimeValue.SetSelectedHighlight(false); }
                SelectedNode.RuntimeValue = node;
                SelectedNode.RuntimeValue.SetSelectedHighlight(true);
                //Debug.Log(node != null ? $"Selected {node.name}" : "No selection");
            }
        }
    }
}