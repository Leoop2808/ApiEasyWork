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
            container.RegisterType<IAuthenticationBO, AuthenticationBO>();
            container.RegisterType<IClienteBO, ClienteBO>();
            container.RegisterType<ITecnicoBO, TecnicoBO>();
            #endregion

            #region DataAccess
            container.RegisterType<IUsuarioDO, UsuarioDO>();
            container.RegisterType<IAuthenticationDO, AuthenticationDO>();
            container.RegisterType<IClienteDO, ClienteDO>();
            container.RegisterType<ITecnicoDO, TecnicoDO>();
            #endregion

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}