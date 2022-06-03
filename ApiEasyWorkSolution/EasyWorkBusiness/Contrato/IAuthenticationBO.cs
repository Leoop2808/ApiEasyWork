using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IAuthenticationBO
    {
        AutenticarGoogleResponse AutenticarGoogle(AutenticarGoogleRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarFacebookResponse AutenticarFacebook(AutenticarFacebookRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappResponse EnviarSmsOrWhatsapp(EnviarSmsOrWhatsappRequest request, string cod_aplicacion, string idLogTexto);
        EnviarCodigoVerificacionCorreoResponse EnviarCodigoVerificacionCorreo(EnviarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        EnviarCorreoCodigoRecuperacionClaveResponse EnviarCorreoCodigoRecuperacionClave(EnviarCorreoCodigoRecuperacionClaveRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCorreoResponse VerificarCodigoVerificacionCorreo(VerificarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCelularResponse VerificarCodigoVerificacionCelular(VerificarCodigoVerificacionCelularRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappAutenticacionResponse EnviarSmsOrWhatsappAutenticacion(EnviarSmsOrWhatsappAutenticacionRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarCelularResponse AutenticarCelular(AutenticarCelularRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarTecnicoResponse AutenticarTecnico(AutenticarTecnicoRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarTecnicoGoogleResponse AutenticarTecnicoGoogle(AutenticarTecnicoGoogleRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarTecnicoFacebookResponse AutenticarTecnicoFacebook(AutenticarTecnicoFacebookRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarTecnicoCelularResponse AutenticarTecnicoCelular(AutenticarTecnicoCelularRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappResponse EnviarSmsOrWhatsappTecnico(EnviarSmsOrWhatsappRequest request, string cod_aplicacion, string idLogTexto);
        EnviarCodigoVerificacionCorreoResponse EnviarCodigoVerificacionCorreoTecnico(EnviarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        EnviarCorreoCodigoRecuperacionClaveResponse EnviarCorreoCodigoRecuperacionClaveTecnico(EnviarCorreoCodigoRecuperacionClaveRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCorreoResponse VerificarCodigoVerificacionCorreoTecnico(VerificarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCelularResponse VerificarCodigoVerificacionCelularTecnico(VerificarCodigoVerificacionCelularRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappAutenticacionResponse EnviarSmsOrWhatsappAutenticacionTecnico(EnviarSmsOrWhatsappAutenticacionRequest request, string cod_aplicacion, string idLogTexto);
        ObtenerIdentificadorTecnicoResponse ObtenerIdentificadorTecnico(string username, string cod_aplicacion, string idLogTexto);
    }
}
