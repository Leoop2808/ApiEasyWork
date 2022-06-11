using EasyWorkEntities.Tecnico.Request;
using EasyWorkEntities.Tecnico.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface ITecnicoBO
    {
        ValidarTecnicoServicioEnProcesoResponse ValidarTecnicoServicioEnProceso(string cod_aplicacion, string cod_usuario, string idLogTexto);
        TecnicoFinalizarCancelarServicioResponse TecnicoFinalizarServicio(TecnicoFinalizarCancelarServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        TecnicoFinalizarCancelarServicioResponse TecnicoCancelarServicio(TecnicoFinalizarCancelarServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        TecnicoObtenerServicioEnProcesoResponse TecnicoObtenerServicioEnProceso(int idServicioEnProceso, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesResponse ObtenerSolicitudes(string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesGeneralesResponse ObtenerSolicitudesGenerales(ObtenerSolicitudesGeneralesRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesDirectasResponse ObtenerSolicitudesDirectas(ObtenerSolicitudesDirectasRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        AceptarSolicitudServicioResponse AceptarSolicitudServicio(int idServicio, string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
