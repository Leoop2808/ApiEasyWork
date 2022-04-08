﻿using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IAuthenticationBO
    {
        AutenticarGoogleResponse AutenticarGoogle(AutenticarGoogleRequest request, string cod_aplicacion, string idLogTexto);
        AutenticarFacebookResponse AutenticarFacebook(AutenticarFacebookRequest request, string cod_aplicacion, string idLogTexto);
    }
}
