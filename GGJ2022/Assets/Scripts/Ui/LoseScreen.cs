using System;
using UnityEngine;

namespace Ui
{
    public class LoseScreen : MonoBehaviour
    {
        public event Action OnPlayAgain;
        public event Action OnBackToMain;
        
        public void SetVisibility(bool active)
        {
            transform.gameObject.SetActive(active);
        }
    }
}