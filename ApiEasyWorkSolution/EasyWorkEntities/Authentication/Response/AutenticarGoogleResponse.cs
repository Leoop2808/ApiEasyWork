﻿namespace EasyWorkEntities.Authentication.Response
{
   public class AutenticarGoogleResponse : GlobalHTTPResponse
   {
        public bool flgMostrarRegistroUsuario { get; set; }
        public bool flgCelularValidado { get; set; }
        public bool flgCorreoValidado { get; set; }
        public AuthenticatedUserData datos { get; set; }
    }
}
