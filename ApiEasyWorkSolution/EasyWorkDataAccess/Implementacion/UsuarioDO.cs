using EasyWorkDataAccess.Contrato;
using log4net;

namespace EasyWorkDataAccess.Implementacion
{
    public class UsuarioDO : IUsuarioDO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioDO));
    }
}
