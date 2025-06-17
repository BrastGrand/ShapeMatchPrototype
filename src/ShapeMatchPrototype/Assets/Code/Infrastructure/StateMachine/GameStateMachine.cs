using Code.Infrastructure.StateMachine.Factory;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private IExitableState _activeState;
        private readonly IStateFactory _stateFactory;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public UniTask Enter<TState>() where TState : class, IState =>
            RequestEnter<TState>();

        public UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
            RequestEnter<TState, TPayload>(payload);

        private async UniTask RequestEnter<TState>() where TState : class, IState
        {
            TState state = await RequestChangeState<TState>();
            EnterState(state);
        }

        private async UniTask RequestEnter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = await RequestChangeState<TState>();
            EnterPayloadState(state, payload);
        }

        private void EnterState<TState>(TState state) where TState : class, IState
        {
            _activeState = state;
            state.Enter();
        }

        private void EnterPayloadState<TState, TPayload>(TState state, TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            _activeState = state;
            state.Enter(payload);
        }

        private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState != null)
            {
                await _activeState.Exit();
                return GetState<TState>();
            }

            return GetState<TState>();
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _stateFactory.GetState<TState>();
    }
}