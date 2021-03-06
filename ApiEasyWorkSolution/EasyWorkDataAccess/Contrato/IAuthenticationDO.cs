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
        ValidarExistenciaUsuarioCorreoResponse ValidarExistenciaUsuarioCorreo(string correo, string cod_aplicacion, string idLogTexto);
        RegistrarCodigoVerificacionResponse RegistrarCodigoVerificacion(string verifyCode, string correo, string nroCelular, string codTipoCodigoVerificacion, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionResponse VerificarCodigoVerificacion(string codigoVerificacion, string correo, string nroCelular, bool flgCelularCorreo, string cod_aplicacion, string idLogTexto);
        VerificarCodigoAutenticacionResponse VerificarCodigoAutenticacion(string codVerificacion, string nroCelular, double latitud, double longitud, string cod_aplicacion, string idLogTexto);
        ValidarExistenciaUsuarioCelularResponse ValidarExistenciaUsuarioCelular(string nroCelular, string cod_aplicacion, string idLogTexto);
        VerificarUsuarioTecnicoResponse VerificarUsuarioTecnico(string usuario, string cod_aplicacion, string idLogTexto);
        RegistrarDatosGoogleResponse RegistrarDatosGoogleTecnico(DataGoogle request, double latitud, double longitud, string google_token, string clientid_aplicacion, string idLogTexto);
        RegistrarDatosFacebookResponse RegistrarDatosFacebookTecnico(DataFacebook request, double latitud, double longitud, string facebook_token, string clientid_aplicacion, string idLogTexto);
        VerificarCodigoAutenticacionResponse VerificarCodigoAutenticacionTecnico(string codVerificacion, string nroCelular, double latitud, double longitud, string cod_aplicacion, string idLogTexto);
        RegistrarCodigoVerificacionResponse RegistrarCodigoVerificacionTecnico(string verifyCode, string correo, string nroCelular, string codTipoCodigoVerificacion, string cod_aplicacion, string idLogTexto);
        ObtenerIdentificadorTecnicoResponse ObtenerIdentificadorTecnico(string username, string cod_aplicacion, string idLogTexto);
        ValidarExistenciaUsuarioCorreoResponse ValidarExistenciaUsuarioCorreoTecnico(string correo, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionResponse VerificarCodigoVerificacionTecnico(string codigoVerificacion, string correo, string nroCelular, bool flgCelularCorreo, string cod_aplicacion, string idLogTexto);
        ValidarExistenciaUsuarioCelularResponse ValidarExistenciaUsuarioCelularTecnico(string nroCelular, string cod_aplicacion, string idLogTexto);
    }
}
