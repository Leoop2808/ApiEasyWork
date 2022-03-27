using EasyWorkBusiness.Contrato;
using log4net;
using System.Web.Http;

namespace ApiEasyWork.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));
        private readonly IUsuarioBO _usuarioBO;
        public UsuarioController(IUsuarioBO usuarioBO)
        {
            _usuarioBO = usuarioBO;
        }
    }
}