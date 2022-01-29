using Board;
using Game;
using UnityEngine;
using Variables;

namespace Ui
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private NodeVariable _selectedNode;
        
        private ResourceHandler _resourceHandler;
        
        public void Init(ResourceHandler resourceHandler)
        {
            _resourceHandler = resourceHandler;
            _selectedNode.OnChanged += HandleSelectedNodeChanged;
        }

        private void HandleSelectedNodeChanged(Node node)
        {
            // todo: check node and update ui elements
        }

        private void OnDestroy()
        {
            _selectedNode.OnChanged -= HandleSelectedNodeChanged;
        }
    }
}