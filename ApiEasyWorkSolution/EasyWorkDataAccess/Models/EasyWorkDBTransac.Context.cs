﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyWorkDataAccess.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EasyWorkDBEntities : DbContext
    {
        public EasyWorkDBEntities()
            : base("name=EasyWorkDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<mst_rol> mst_rol { get; set; }
        public virtual DbSet<trs_usuario> trs_usuario { get; set; }
        public virtual DbSet<mst_aplicacion> mst_aplicacion { get; set; }
    
        public virtual ObjectResult<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result> SP_OBTENER_DATA_PRINCIPAL_USUARIO(Nullable<int> idUsuario, string codMedioAcceso)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("idUsuario", idUsuario) :
                new ObjectParameter("idUsuario", typeof(int));
    
            var codMedioAccesoParameter = codMedioAcceso != null ?
                new ObjectParameter("codMedioAcceso", codMedioAcceso) :
                new ObjectParameter("codMedioAcceso", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result>("SP_OBTENER_DATA_PRINCIPAL_USUARIO", idUsuarioParameter, codMedioAccesoParameter);
        }
    
        public virtual ObjectResult<SP_REGISTRAR_DATOS_FACEBOOK_Result> SP_REGISTRAR_DATOS_FACEBOOK(string idUserFacebook, string firstName, string lastName, string username, string email, Nullable<bool> pictureIsSilhouette, string pictureUrl, string tokenData, Nullable<decimal> latitud, Nullable<decimal> longitud, string codAplicacion)
        {
            var idUserFacebookParameter = idUserFacebook != null ?
                new ObjectParameter("idUserFacebook", idUserFacebook) :
                new ObjectParameter("idUserFacebook", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var pictureIsSilhouetteParameter = pictureIsSilhouette.HasValue ?
                new ObjectParameter("pictureIsSilhouette", pictureIsSilhouette) :
                new ObjectParameter("pictureIsSilhouette", typeof(bool));
    
            var pictureUrlParameter = pictureUrl != null ?
                new ObjectParameter("pictureUrl", pictureUrl) :
                new ObjectParameter("pictureUrl", typeof(string));
    
            var tokenDataParameter = tokenData != null ?
                new ObjectParameter("tokenData", tokenData) :
                new ObjectParameter("tokenData", typeof(string));
    
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("latitud", latitud) :
                new ObjectParameter("latitud", typeof(decimal));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("longitud", longitud) :
                new ObjectParameter("longitud", typeof(decimal));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_DATOS_FACEBOOK_Result>("SP_REGISTRAR_DATOS_FACEBOOK", idUserFacebookParameter, firstNameParameter, lastNameParameter, usernameParameter, emailParameter, pictureIsSilhouetteParameter, pictureUrlParameter, tokenDataParameter, latitudParameter, longitudParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_REGISTRAR_DATOS_GOOGLE_Result> SP_REGISTRAR_DATOS_GOOGLE(string idUserGoogle, string email, Nullable<bool> verified_email, string name, string given_name, string family_name, string pictureUrl, string locale, string tokenData, Nullable<decimal> latitud, Nullable<decimal> longitud, string codAplicacion)
        {
            var idUserGoogleParameter = idUserGoogle != null ?
                new ObjectParameter("idUserGoogle", idUserGoogle) :
                new ObjectParameter("idUserGoogle", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var verified_emailParameter = verified_email.HasValue ?
                new ObjectParameter("verified_email", verified_email) :
                new ObjectParameter("verified_email", typeof(bool));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var given_nameParameter = given_name != null ?
                new ObjectParameter("given_name", given_name) :
                new ObjectParameter("given_name", typeof(string));
    
            var family_nameParameter = family_name != null ?
                new ObjectParameter("family_name", family_name) :
                new ObjectParameter("family_name", typeof(string));
    
            var pictureUrlParameter = pictureUrl != null ?
                new ObjectParameter("pictureUrl", pictureUrl) :
                new ObjectParameter("pictureUrl", typeof(string));
    
            var localeParameter = locale != null ?
                new ObjectParameter("locale", locale) :
                new ObjectParameter("locale", typeof(string));
    
            var tokenDataParameter = tokenData != null ?
                new ObjectParameter("tokenData", tokenData) :
                new ObjectParameter("tokenData", typeof(string));
    
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("latitud", latitud) :
                new ObjectParameter("latitud", typeof(decimal));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("longitud", longitud) :
                new ObjectParameter("longitud", typeof(decimal));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_DATOS_GOOGLE_Result>("SP_REGISTRAR_DATOS_GOOGLE", idUserGoogleParameter, emailParameter, verified_emailParameter, nameParameter, given_nameParameter, family_nameParameter, pictureUrlParameter, localeParameter, tokenDataParameter, latitudParameter, longitudParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_SEGURIDAD_ATTRIBUTE_VALIDAR_APLICACION_Result> SP_SEGURIDAD_ATTRIBUTE_VALIDAR_APLICACION(string nombreAplicacion, string accesId)
        {
            var nombreAplicacionParameter = nombreAplicacion != null ?
                new ObjectParameter("nombreAplicacion", nombreAplicacion) :
                new ObjectParameter("nombreAplicacion", typeof(string));
    
            var accesIdParameter = accesId != null ?
                new ObjectParameter("accesId", accesId) :
                new ObjectParameter("accesId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SEGURIDAD_ATTRIBUTE_VALIDAR_APLICACION_Result>("SP_SEGURIDAD_ATTRIBUTE_VALIDAR_APLICACION", nombreAplicacionParameter, accesIdParameter);
        }
    
        public virtual ObjectResult<SP_REGISTRAR_CODIGO_VERIFICACION_Result> SP_REGISTRAR_CODIGO_VERIFICACION(string verifyCode, string correo, string nroCelular, Nullable<bool> flgCelular, Nullable<bool> flgCorreo, Nullable<bool> flgEnviadoSms)
        {
            var verifyCodeParameter = verifyCode != null ?
                new ObjectParameter("verifyCode", verifyCode) :
                new ObjectParameter("verifyCode", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var nroCelularParameter = nroCelular != null ?
                new ObjectParameter("nroCelular", nroCelular) :
                new ObjectParameter("nroCelular", typeof(string));
    
            var flgCelularParameter = flgCelular.HasValue ?
                new ObjectParameter("flgCelular", flgCelular) :
                new ObjectParameter("flgCelular", typeof(bool));
    
            var flgCorreoParameter = flgCorreo.HasValue ?
                new ObjectParameter("flgCorreo", flgCorreo) :
                new ObjectParameter("flgCorreo", typeof(bool));
    
            var flgEnviadoSmsParameter = flgEnviadoSms.HasValue ?
                new ObjectParameter("flgEnviadoSms", flgEnviadoSms) :
                new ObjectParameter("flgEnviadoSms", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_CODIGO_VERIFICACION_Result>("SP_REGISTRAR_CODIGO_VERIFICACION", verifyCodeParameter, correoParameter, nroCelularParameter, flgCelularParameter, flgCorreoParameter, flgEnviadoSmsParameter);
        }
    }
}
