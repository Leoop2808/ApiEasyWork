﻿namespace EasyWorkEntities.Authentication.Response
{
    public class RegistrarDatosGoogleResponse : GlobalHTTPResponse
    {
        public string codUsuarioCreado { get; set; }
        public bool flgMostrarRegistroUsuario { get; set; }
    }
}
