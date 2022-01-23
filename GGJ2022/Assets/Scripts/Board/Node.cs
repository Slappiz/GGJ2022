using UnityEngine;

namespace Board
{
    public enum NodeType
    {
        None =  default,
        Root = 1,
        Resource = 2,
        Fort = 3,
    }
    
    public class Node : MonoBehaviour
    {
        public NodeType Type { get; private set; }

        public void Setup(AbstractNodeBlueprint blueprint)
        {
            // Todo :  set type
        }
    }
}