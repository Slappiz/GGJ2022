using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class LoseScreen : MonoBehaviour
    {
        public event Action OnPlayAgain;
        public event Action OnBackToMain;
        
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainButton;

        
        public void SetVisibility(bool active)
        {
            transform.gameObject.SetActive(active);
        }
        
        private void Start()
        {
            _retryButton.onClick.AddListener(HandleRetry);
            _mainButton.onClick.AddListener(HandleMain);
        }

        private void OnDestroy()
        {
            _retryButton.onClick.RemoveListener(HandleRetry);
            _mainButton.onClick.RemoveListener(HandleMain);
        }

        private void HandleMain()
        {
            OnBackToMain?.Invoke();
        }

        private void HandleRetry()
        {
            OnPlayAgain?.Invoke();
        }
    }
}