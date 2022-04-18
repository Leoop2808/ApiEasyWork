using EasyWorkEntities.Cliente.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IClienteBO
    {
        ObtenerListaMaestrosResponse ObtenerListaMaestros(string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
