using System.Linq;
using Board;
using Game;
using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace Ui
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private NodeVariable _selectedNode;

        [Header("Buttons")] 
        [SerializeField] private Button _claimNodeButton;
        [SerializeField] private Button _nukeNodeButton;
        [SerializeField] private Button _scoutNodeButton;
        
        private GameLogic _logic;
        public void Init(GameLogic logic)
        {
            _logic = logic;
            
            _selectedNode.OnChanged += HandleSelectedNodeChanged;
            _claimNodeButton.onClick.AddListener(HandleClaimClick);
            _nukeNodeButton.onClick.AddListener(HandleNukeClick);
            _scoutNodeButton.onClick.AddListener(HandleScoutClick);
            
            HandleSelectedNodeChanged(_selectedNode.RuntimeValue);
        }

        private void RefreshAllButtonTexts()
        {
            RefreshButtonText(_claimNodeButton, $"Claim");
            RefreshButtonText(_nukeNodeButton, $"Nuke ({_logic.NukeCharges})");
            RefreshButtonText(_scoutNodeButton, $"Scout ({_logic.ScoutCharges})");
        }
        
        private void RefreshButtonText(Button button, string text)
        {
            var t = button.GetComponentInChildren<Text>();
            t.text = text;
        }

        private void HandleNukeClick()
        {
            _logic.NukeNode(_selectedNode.RuntimeValue);
            HandleSelectedNodeChanged(_selectedNode.RuntimeValue); // Refresh ui
        }

        private void HandleScoutClick()
        {
            _logic.ScoutNode(_selectedNode.RuntimeValue);
            HandleSelectedNodeChanged(_selectedNode.RuntimeValue); // Refresh ui
        }

        private void HandleClaimClick()
        {
            _logic.ClaimNode(_selectedNode.RuntimeValue);
            HandleSelectedNodeChanged(_selectedNode.RuntimeValue); // Refresh ui
        }

        private void CheckClaim(Node node)
        {
            _claimNodeButton.interactable = _logic.CanClaim(node);
        }

        private void CheckNuke(Node node)
        {
            _nukeNodeButton.interactable = _logic.CanNuke(node);
        }
        
        private void CheckScout(Node node)
        {
            _scoutNodeButton.interactable = _logic.CanScout(node);
        }

        private void HandleSelectedNodeChanged(Node node)
        {
            RefreshAllButtonTexts();
            if (node == null)
            {
                _claimNodeButton.interactable = false;
                _nukeNodeButton.interactable = false;
                _scoutNodeButton.interactable = false;
                return;
            }
            
            CheckClaim(node);
            CheckNuke(node);
            CheckScout(node);
        }

        private void OnDestroy()
        {
            _selectedNode.OnChanged -= HandleSelectedNodeChanged;
            _claimNodeButton.onClick.RemoveListener(HandleClaimClick);
            _nukeNodeButton.onClick.RemoveListener(HandleNukeClick);
            _scoutNodeButton.onClick.RemoveListener(HandleScoutClick);
        }
    }
}