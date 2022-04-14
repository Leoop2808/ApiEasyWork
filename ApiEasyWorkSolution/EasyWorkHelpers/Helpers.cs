using EasyWorkEntities.Helpers.Request;
using EasyWorkEntities.Helpers.Response;
using System;
using System.Net;
using System.Net.Mail;

namespace EasyWorkHelpers
{
    public class Helpers
    {
        public static string GenerateCode(int p_CodeLength)
        {
            string result = "";
            string pattern = "01234567890123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            Random myRndGenerator = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < p_CodeLength; i++)
            {
                int mIndex = myRndGenerator.Next(pattern.Length);
                result += pattern[mIndex];
            }

            return result;
        }

        public static EnviarCorreoResponse EnviarCorreo(EnviarCorreoRequest request)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(request.correo);
                mail.Subject = request.asunto;
                mail.Body = request.body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient();
                client.Send(mail);

                return new EnviarCorreoResponse()
                {
                    codeRes = HttpStatusCode.OK,
                    messageRes = "Correo enviado"
                };
            }
            catch (Exception e)
            {
                return new EnviarCorreoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar correo"
                };
            }
        }
    }
}
