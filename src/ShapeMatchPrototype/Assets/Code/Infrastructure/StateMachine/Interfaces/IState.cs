using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}