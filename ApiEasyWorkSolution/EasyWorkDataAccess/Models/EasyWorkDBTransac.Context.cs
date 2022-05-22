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
    
        public virtual ObjectResult<SP_REGISTRAR_CODIGO_VERIFICACION_Result> SP_REGISTRAR_CODIGO_VERIFICACION(string verifyCode, string correo, string nroCelular, string codTipoCodigoVerificacion)
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
    
            var codTipoCodigoVerificacionParameter = codTipoCodigoVerificacion != null ?
                new ObjectParameter("codTipoCodigoVerificacion", codTipoCodigoVerificacion) :
                new ObjectParameter("codTipoCodigoVerificacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_CODIGO_VERIFICACION_Result>("SP_REGISTRAR_CODIGO_VERIFICACION", verifyCodeParameter, correoParameter, nroCelularParameter, codTipoCodigoVerificacionParameter);
        }
    
        public virtual ObjectResult<SP_VERIFICAR_CODIGO_VERIFICACION_Result> SP_VERIFICAR_CODIGO_VERIFICACION(string codigoVerificacion, string correo, string nroCelular, Nullable<bool> flgCelularCorreo)
        {
            var codigoVerificacionParameter = codigoVerificacion != null ?
                new ObjectParameter("codigoVerificacion", codigoVerificacion) :
                new ObjectParameter("codigoVerificacion", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var nroCelularParameter = nroCelular != null ?
                new ObjectParameter("nroCelular", nroCelular) :
                new ObjectParameter("nroCelular", typeof(string));
    
            var flgCelularCorreoParameter = flgCelularCorreo.HasValue ?
                new ObjectParameter("flgCelularCorreo", flgCelularCorreo) :
                new ObjectParameter("flgCelularCorreo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_VERIFICAR_CODIGO_VERIFICACION_Result>("SP_VERIFICAR_CODIGO_VERIFICACION", codigoVerificacionParameter, correoParameter, nroCelularParameter, flgCelularCorreoParameter);
        }
    
        public virtual ObjectResult<SP_OBTENER_DATA_SESION_X_USUARIO_Result> SP_OBTENER_DATA_SESION_X_USUARIO(Nullable<int> idUsuario)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("idUsuario", idUsuario) :
                new ObjectParameter("idUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_DATA_SESION_X_USUARIO_Result>("SP_OBTENER_DATA_SESION_X_USUARIO", idUsuarioParameter);
        }
    
        public virtual ObjectResult<SP_OBTENER_ROL_X_COD_ROL_Result> SP_OBTENER_ROL_X_COD_ROL(string codRol)
        {
            var codRolParameter = codRol != null ?
                new ObjectParameter("codRol", codRol) :
                new ObjectParameter("codRol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_ROL_X_COD_ROL_Result>("SP_OBTENER_ROL_X_COD_ROL", codRolParameter);
        }
    
        public virtual ObjectResult<SP_REGISTRAR_PERSONA_Result> SP_REGISTRAR_PERSONA(string nombre1, string nombre2, string apellido1, string apellido2, string celular, string correo, string codDistrito, string codTipoDocumento, string documento, string genero, Nullable<decimal> latitud, Nullable<decimal> longitud, string codAplicacion)
        {
            var nombre1Parameter = nombre1 != null ?
                new ObjectParameter("nombre1", nombre1) :
                new ObjectParameter("nombre1", typeof(string));
    
            var nombre2Parameter = nombre2 != null ?
                new ObjectParameter("nombre2", nombre2) :
                new ObjectParameter("nombre2", typeof(string));
    
            var apellido1Parameter = apellido1 != null ?
                new ObjectParameter("apellido1", apellido1) :
                new ObjectParameter("apellido1", typeof(string));
    
            var apellido2Parameter = apellido2 != null ?
                new ObjectParameter("apellido2", apellido2) :
                new ObjectParameter("apellido2", typeof(string));
    
            var celularParameter = celular != null ?
                new ObjectParameter("celular", celular) :
                new ObjectParameter("celular", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var codDistritoParameter = codDistrito != null ?
                new ObjectParameter("codDistrito", codDistrito) :
                new ObjectParameter("codDistrito", typeof(string));
    
            var codTipoDocumentoParameter = codTipoDocumento != null ?
                new ObjectParameter("codTipoDocumento", codTipoDocumento) :
                new ObjectParameter("codTipoDocumento", typeof(string));
    
            var documentoParameter = documento != null ?
                new ObjectParameter("documento", documento) :
                new ObjectParameter("documento", typeof(string));
    
            var generoParameter = genero != null ?
                new ObjectParameter("genero", genero) :
                new ObjectParameter("genero", typeof(string));
    
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("latitud", latitud) :
                new ObjectParameter("latitud", typeof(decimal));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("longitud", longitud) :
                new ObjectParameter("longitud", typeof(decimal));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_PERSONA_Result>("SP_REGISTRAR_PERSONA", nombre1Parameter, nombre2Parameter, apellido1Parameter, apellido2Parameter, celularParameter, correoParameter, codDistritoParameter, codTipoDocumentoParameter, documentoParameter, generoParameter, latitudParameter, longitudParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE_Result> SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE(string correo, string celular)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var celularParameter = celular != null ?
                new ObjectParameter("celular", celular) :
                new ObjectParameter("celular", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE_Result>("SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE", correoParameter, celularParameter);
        }
    
        public virtual ObjectResult<SP_OBTENER_DISTRITOS_Result> SP_OBTENER_DISTRITOS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_DISTRITOS_Result>("SP_OBTENER_DISTRITOS");
        }
    
        public virtual ObjectResult<SP_OBTENER_MEDIOS_PAGO_Result> SP_OBTENER_MEDIOS_PAGO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_MEDIOS_PAGO_Result>("SP_OBTENER_MEDIOS_PAGO");
        }
    
        public virtual ObjectResult<SP_OBTENER_TIPOS_BUSQUEDA_Result> SP_OBTENER_TIPOS_BUSQUEDA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_TIPOS_BUSQUEDA_Result>("SP_OBTENER_TIPOS_BUSQUEDA");
        }
    
        public virtual ObjectResult<SP_OBTENER_TIPOS_DOCUMENTO_Result> SP_OBTENER_TIPOS_DOCUMENTO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_TIPOS_DOCUMENTO_Result>("SP_OBTENER_TIPOS_DOCUMENTO");
        }
    
        public virtual ObjectResult<SP_OBTENER_TIPOS_TRANSPORTE_Result> SP_OBTENER_TIPOS_TRANSPORTE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_TIPOS_TRANSPORTE_Result>("SP_OBTENER_TIPOS_TRANSPORTE");
        }
    
        public virtual ObjectResult<SP_VALIDAR_EXISTENCIA_USUARIO_CORREO_Result> SP_VALIDAR_EXISTENCIA_USUARIO_CORREO(string correo, string codAplicacion)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_VALIDAR_EXISTENCIA_USUARIO_CORREO_Result>("SP_VALIDAR_EXISTENCIA_USUARIO_CORREO", correoParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_REGISTRAR_DISPOSITIVO_Result> SP_REGISTRAR_DISPOSITIVO(string keyDispositivo, string versionAndroid, string versionApp, Nullable<decimal> latitud, Nullable<decimal> longitud, string codUsuario, string codAplicacion)
        {
            var keyDispositivoParameter = keyDispositivo != null ?
                new ObjectParameter("keyDispositivo", keyDispositivo) :
                new ObjectParameter("keyDispositivo", typeof(string));
    
            var versionAndroidParameter = versionAndroid != null ?
                new ObjectParameter("versionAndroid", versionAndroid) :
                new ObjectParameter("versionAndroid", typeof(string));
    
            var versionAppParameter = versionApp != null ?
                new ObjectParameter("versionApp", versionApp) :
                new ObjectParameter("versionApp", typeof(string));
    
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("latitud", latitud) :
                new ObjectParameter("latitud", typeof(decimal));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("longitud", longitud) :
                new ObjectParameter("longitud", typeof(decimal));
    
            var codUsuarioParameter = codUsuario != null ?
                new ObjectParameter("codUsuario", codUsuario) :
                new ObjectParameter("codUsuario", typeof(string));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_REGISTRAR_DISPOSITIVO_Result>("SP_REGISTRAR_DISPOSITIVO", keyDispositivoParameter, versionAndroidParameter, versionAppParameter, latitudParameter, longitudParameter, codUsuarioParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_VERIFICAR_CODIGO_AUTENTICACION_Result> SP_VERIFICAR_CODIGO_AUTENTICACION(string codigoVerificacion, string nroCelular, Nullable<decimal> latitud, Nullable<decimal> longitud)
        {
            var codigoVerificacionParameter = codigoVerificacion != null ?
                new ObjectParameter("codigoVerificacion", codigoVerificacion) :
                new ObjectParameter("codigoVerificacion", typeof(string));
    
            var nroCelularParameter = nroCelular != null ?
                new ObjectParameter("nroCelular", nroCelular) :
                new ObjectParameter("nroCelular", typeof(string));
    
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("latitud", latitud) :
                new ObjectParameter("latitud", typeof(decimal));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("longitud", longitud) :
                new ObjectParameter("longitud", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_VERIFICAR_CODIGO_AUTENTICACION_Result>("SP_VERIFICAR_CODIGO_AUTENTICACION", codigoVerificacionParameter, nroCelularParameter, latitudParameter, longitudParameter);
        }
    
        public virtual ObjectResult<SP_VALIDAR_EXISTENCIA_USUARIO_CELULAR_Result> SP_VALIDAR_EXISTENCIA_USUARIO_CELULAR(string nroCelular, string codAplicacion)
        {
            var nroCelularParameter = nroCelular != null ?
                new ObjectParameter("nroCelular", nroCelular) :
                new ObjectParameter("nroCelular", typeof(string));
    
            var codAplicacionParameter = codAplicacion != null ?
                new ObjectParameter("codAplicacion", codAplicacion) :
                new ObjectParameter("codAplicacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_VALIDAR_EXISTENCIA_USUARIO_CELULAR_Result>("SP_VALIDAR_EXISTENCIA_USUARIO_CELULAR", nroCelularParameter, codAplicacionParameter);
        }
    
        public virtual ObjectResult<SP_OBTENER_CATEGORIAS_SERVICIO_Result> SP_OBTENER_CATEGORIAS_SERVICIO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_OBTENER_CATEGORIAS_SERVICIO_Result>("SP_OBTENER_CATEGORIAS_SERVICIO");
        }
    }
}
