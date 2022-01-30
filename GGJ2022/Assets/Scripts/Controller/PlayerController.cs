using Board;
using UnityEngine;
using UnityEngine.EventSystems;
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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (HoverNode.RuntimeValue == null) return;
                HoverNode.RuntimeValue.SetHoverHighlight(false);
                HoverNode.RuntimeValue = null;
                return;
            }
            
            Hover();

            if (Input.GetMouseButtonDown(0))
            {
                if (HoverNode.RuntimeValue != null)
                {
                    if(SelectedNode.RuntimeValue == HoverNode.RuntimeValue) return;
                    if (SelectedNode.RuntimeValue != null) { SelectedNode.RuntimeValue.SetSelectedHighlight(false); }
                    SelectedNode.RuntimeValue = HoverNode.RuntimeValue;
                    SelectedNode.RuntimeValue.SetSelectedHighlight(true);
                }
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
            }
        }
    }
}