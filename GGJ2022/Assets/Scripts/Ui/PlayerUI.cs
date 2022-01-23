using Game;
using UnityEngine;

namespace Ui
{
    public class PlayerUI : MonoBehaviour
    {
        private ResourceHandler _resourceHandler;

        public void Init(ResourceHandler resourceHandler)
        {
            _resourceHandler = resourceHandler;
        }
    }
}