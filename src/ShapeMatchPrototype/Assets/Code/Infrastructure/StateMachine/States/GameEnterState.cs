using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine.States
{
    public class GameEnterState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public GameEnterState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            //world initialize
            await _stateMachine.Enter<GameLoopState>();
        }

        public UniTask Exit() => default;
    }
}