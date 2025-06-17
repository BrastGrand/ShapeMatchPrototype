using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}