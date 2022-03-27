using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using log4net;

namespace EasyWorkBusiness.Implementacion
{
    public class UsuarioBO : IUsuarioBO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioBO));
        readonly IUsuarioDO _usuarioDO;
        public UsuarioBO(IUsuarioDO usuarioDO)
        {
            _usuarioDO = usuarioDO;
        }
    }
}
