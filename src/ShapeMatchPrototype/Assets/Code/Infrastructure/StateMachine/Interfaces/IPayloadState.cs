using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachine
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}