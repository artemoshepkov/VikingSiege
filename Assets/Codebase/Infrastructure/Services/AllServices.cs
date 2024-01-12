using Codebase.Infrastructure.Services.StaticData;
using Codebase.Infrastructure.States;

namespace Codebase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void Register<TService>(TService service) where TService : IService =>
            Implementation<TService>.Service = service;

        public TService Single<TService>() where TService : IService => Implementation<TService>.Service;

        private static class Implementation<TService> where TService : IService
        {
            public static TService Service;
        }
    }
}