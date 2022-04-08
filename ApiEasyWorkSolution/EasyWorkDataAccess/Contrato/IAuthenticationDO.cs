using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;

namespace EasyWorkDataAccess.Contrato
{
    public interface IAuthenticationDO
    {
        RegistrarDatosGoogleResponse RegistrarDatosGoogle(AutenticarGoogleRequest request,string clientid_aplicacion,string idLogTexto);
        RegistrarDatosFacebookResponse RegistrarDatosFacebook(AutenticarFacebookRequest request, string clientid_aplicacion, string idLogTexto);
        ObtenerDataPrincipalUsuarioResponse ObtenerDataPrincipalUsuario(string codUsuarioCreado, string codMedioAcceso, string clientid_aplicacion, string idLogTexto);
    }
}
