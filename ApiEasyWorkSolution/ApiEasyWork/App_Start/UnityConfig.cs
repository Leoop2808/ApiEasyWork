using EasyWorkBusiness.Contrato;
using EasyWorkBusiness.Implementacion;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Implementacion;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ApiEasyWork
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            #region Business
            container.RegisterType<IUsuarioBO, UsuarioBO>();
            #endregion

            #region DataAccess
            container.RegisterType<IUsuarioDO, UsuarioDO>();
            #endregion

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}