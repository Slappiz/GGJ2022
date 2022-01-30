using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _howToButton;
        [SerializeField] private Transform _buttonContainer;
        
        [SerializeField] private Transform _howToContainer;
        [SerializeField] private Button _closeHowToButton;
        private void Start()
        {
            _howToContainer.gameObject.SetActive(false);
            _playButton.onClick.AddListener(HandlePlay);
            _howToButton.onClick.AddListener(HandleHowTo);
            _closeHowToButton.onClick.AddListener(HandleClose);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(HandlePlay);
            _howToButton.onClick.RemoveListener(HandleHowTo);
            _closeHowToButton.onClick.RemoveListener(HandleClose);
        }

        private void HandleClose()
        {
            _buttonContainer.gameObject.SetActive(true);
            _howToContainer.gameObject.SetActive(false);
        }

        private void HandlePlay()
        {
            SceneManager.LoadScene("Game");
        }

        private void HandleHowTo()
        {
            _buttonContainer.gameObject.SetActive(false);
            _howToContainer.gameObject.SetActive(true);
        }
    }
}