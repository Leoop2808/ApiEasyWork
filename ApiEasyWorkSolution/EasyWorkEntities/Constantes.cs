namespace EasyWorkEntities
{
    public class Constantes
    {
        public class MedioAcceso 
        {
            public const string COD_AUTH_CELULAR = "1";
            public const string COD_AUTH_FACEBOOK = "2";
            public const string COD_AUTH_GMAIL = "3";
            public const string COD_AUTH_CORREO_DEFAULT = "4";
        }
        public class TipoEnvioCodigoVerificacion 
        {
            public const string SMS = "SMS";
            public const string WHATSAPP = "WHATSAPP";
            public const string TWILIO_SMS = "sms";
            public const string TWILIO_WHATSAPP = "whatsapp";
        }

        public class ContentMessageVerifyCode 
        {
            public const string BODY_SMS = "Su código de verificación es @verifyCode";
            public const string BODY_WHATSAPP = "Su código de verificación es *@verifyCode*";
        }

        public class BodyEmails 
        {
            public const string VALIDATION_EMAIL_VERIFY_CODE = "Su código de verificación es @verifyCode";
        }
    }
}
