using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private Button _restartGame;

        public event Action OnRestartGameClicked;

        protected override void Awake()
        {
            base.Awake();
            _restartGame.onClick.AddListener(RestartGameClickHandler);
        }

        private void RestartGameClickHandler()
        {
            OnRestartGameClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _restartGame.onClick.RemoveListener(RestartGameClickHandler);
        }
    }
}