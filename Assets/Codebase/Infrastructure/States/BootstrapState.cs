using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagment;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.Progress;
using Codebase.Infrastructure.Services.Random;
using Codebase.Infrastructure.Services.StaticData;

namespace Codebase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private const string MainSceneName = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LevelLoadState, string>(MainSceneName);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticDataService();
            _allServices.Register<IAsset>(new AssetProvider());
            _allServices.Register<IPersistentProgressService>(new PersistentProgressService());
            _allServices.Register<IRandomService>(new UnityRandomService());
            _allServices.Register<IGameFactory>(new GameFactory(_allServices.Single<IAsset>(),
                _allServices.Single<IStaticDataService>(), _allServices.Single<IRandomService>(),
                _allServices.Single<IPersistentProgressService>()));
        }

        private void RegisterStaticDataService()
        {
            _allServices.Register<IStaticDataService>(new StaticDataService());
            _allServices.Single<IStaticDataService>().LoadData();
        }
    }
}