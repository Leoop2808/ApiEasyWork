using EasyWorkEntities.Cliente.Request;
using EasyWorkEntities.Cliente.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IClienteBO
    {
        ObtenerListaMaestrosResponse ObtenerListaMaestros(string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerListaTecnicosGeneralResponse ObtenerListaTecnicosGeneral(ObtenerListaTecnicosGeneralRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerPerfilTecnicoResponse ObtenerPerfilTecnico(ObtenerPerfilTecnicoRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        RegistrarSolicitudServicioResponse RegistrarSolicitudServicio(RegistrarSolicitudServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
