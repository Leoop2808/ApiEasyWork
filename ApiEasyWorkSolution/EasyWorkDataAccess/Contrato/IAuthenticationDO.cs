using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;

namespace EasyWorkDataAccess.Contrato
{
    public interface IAuthenticationDO
    {
        DataGoogleResponse ObtenerDataGoogle(string google_token,string cod_aplicacion, string idLogTexto);
        DataFacebookResponse ObtenerDataFacebook(string facebook_token, string cod_aplicacion, string idLogTexto);
        RegistrarDatosGoogleResponse RegistrarDatosGoogle(DataGoogle request, double latitud, double longitud, string google_token, string clientid_aplicacion,string idLogTexto);
        RegistrarDatosFacebookResponse RegistrarDatosFacebook(DataFacebook request, double latitud, double longitud, string facebook_token, string clientid_aplicacion, string idLogTexto);
        ObtenerDataPrincipalUsuarioResponse ObtenerDataPrincipalUsuario(string codUsuarioCreado, int idUsuario, string codMedioAcceso, string clientid_aplicacion, string idLogTexto);
        RegistrarCodigoVerificacionResponse RegistrarCodigoVerificacion(string verifyCode, string correo, string nroCelular, bool flgCelular, bool flgCorreo, bool flgEnviadoSms,string cod_aplicacion, string idLogTexto);
    }
}
