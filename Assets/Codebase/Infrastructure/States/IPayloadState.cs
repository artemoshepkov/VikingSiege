namespace Codebase.Infrastructure.States
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload arg);
    }
}