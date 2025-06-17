using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            await _assetProvider.InitializeAsync();
            await _stateMachine.Enter<LoadingGameState>();
        }

        public UniTask Exit() => default;
    }
}