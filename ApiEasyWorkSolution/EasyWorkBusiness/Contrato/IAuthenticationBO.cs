﻿using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IAuthenticationBO
    {
        AutenticarGoogleResponse AutenticarGoogle(AutenticarGoogleRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarFacebookResponse AutenticarFacebook(AutenticarFacebookRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappResponse EnviarSmsOrWhatsapp(EnviarSmsOrWhatsappRequest request,string cod_aplicacion, string idLogTexto);
        EnviarCodigoVerificacionCorreoResponse EnviarCodigoVerificacionCorreo(EnviarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        EnviarCorreoCodigoRecuperacionClaveResponse EnviarCorreoCodigoRecuperacionClave(EnviarCorreoCodigoRecuperacionClaveRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCorreoResponse VerificarCodigoVerificacionCorreo(VerificarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto);
        VerificarCodigoVerificacionCelularResponse VerificarCodigoVerificacionCelular(VerificarCodigoVerificacionCelularRequest request, string cod_aplicacion, string idLogTexto);
        EnviarSmsOrWhatsappAutenticacionResponse EnviarSmsOrWhatsappAutenticacion(EnviarSmsOrWhatsappAutenticacionRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarCelularResponse AutenticarCelular(AutenticarCelularRequest request, string cod_aplicacion, string idLogTexto);
    }
}
