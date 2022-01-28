using Board;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public bool Enabled { get; set; }
        public Node SelectedNode { get; private set; }
        public Node HoverTarget { get; private set; }
        void Update()
        {
            if (!Enabled) return;

            Hover();
            // Move this object to the position clicked by the mouse.
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
                if (HoverTarget == node) return;
                if (HoverTarget != null) { HoverTarget.SetHoverHighlight(false); }
                HoverTarget = node;
                HoverTarget.SetHoverHighlight(true);
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
                SelectedNode.SetSelectedHighlight(false);
                SelectedNode = node;
                SelectedNode.SetSelectedHighlight(true);
                //Debug.Log(node != null ? $"Selected {node.name}" : "No selection");
            }
        }
    }
}