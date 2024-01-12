using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;

namespace Codebase.Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices allServices)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, allServices),
                [typeof(LevelLoadState)] = new LevelLoadState(this, sceneLoader, loadingCurtain, allServices.Single<IGameFactory>(), allServices.Single<IStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload arg) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(arg);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetType<TState>();
            _activeState = state;

            return state;
        }

        private TState GetType<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}