using System;
using Code.Infrastructure.Loading;
using Code.Services.UIService;
using Code.UI;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadingGameState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        private IScreen _screen;

        private const float _MIN_LOADING_TIME = 1.5f;
        private const string _SCENE_NAME = "GameScene";

        public LoadingGameState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public async UniTask Enter()
        {
            _screen = await _uiFactory.CreateScreen<LoadingScreen>("LoadingScreen");
            await _screen.Show();

            var sceneLoadingTask = _sceneLoader.LoadScene(_SCENE_NAME);
            var minLoadingTimeTask = UniTask.Delay(TimeSpan.FromSeconds(_MIN_LOADING_TIME));

            await UniTask.WhenAll(sceneLoadingTask, minLoadingTimeTask);
            await _stateMachine.Enter<GameEnterState>();
        }

        public async UniTask Exit()
        {
            if (_sceneLoader != null)
            {
                await _screen.Hide();
                _uiFactory.CloseScreen<LoadingScreen>();
            }
        }
    }
}