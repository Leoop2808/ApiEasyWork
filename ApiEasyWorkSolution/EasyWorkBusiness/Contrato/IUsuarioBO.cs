using EasyWorkEntities.Usuario.Request;
using EasyWorkEntities.Usuario.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IUsuarioBO
    {
        ValidarExistenciaUsuarioResponse ValidarExistenciaUsuario(string correo, string celular, string cod_aplicacion, string idLogTexto);
        ObtenerRolPorCodRolResponse ObtenerRolPorCodRol(string codRol, string cod_aplicacion,string idLogTexto);
        RegistrarPersonaResponse RegistrarPersona(DataPersona request, string cod_aplicacion, string idLogTexto);
        ObtenerDataSesionResponse ObtenerDataSesion(int id_usuario, string cod_aplicacion, string idLogTexto);
    }
}
