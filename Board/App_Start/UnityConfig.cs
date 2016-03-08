using Microsoft.Practices.Unity;
using System.Web.Http;
using Board.Interfaces;
using Board.Repositories;
using Unity.WebApi;

namespace Board
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IBoardsRepository, BoardsRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}