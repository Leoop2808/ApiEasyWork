using ApiEasyWork.Util;
using EasyWorkDataAccess.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiEasyWork.FiltersAttributes
{
    public class ApplicationAuthenticationFilter : Attribute, IActionFilter
    {
        public ApplicationAuthenticationFilter()
        {

        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
           Func<Task<HttpResponseMessage>> continuation)
        {

            var request = actionContext.Request.Headers.Authorization;
            var identity = ParseAuthorizationHeader(actionContext);
            if (!identity)
            {
                var resultUnauthorized = new HttpResponseMessage();
                resultUnauthorized.StatusCode = HttpStatusCode.Unauthorized;
                var result1 = Task.Run(() => resultUnauthorized);
                result1.Wait();
                return result1;
            }

            var result = continuation();
            result.Wait();

            return result;
        }
        public bool AllowMultiple
        {
            get { return true; }
        }
        protected virtual bool ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            var ctx = new EasyWorkDBEntities();

            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;

            if (auth != null && auth.Scheme == "Basic") authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader)) return false;

            var decodeauthToken = System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(authHeader));

            var credentials = decodeauthToken.Split(':');
            var clientname = credentials[0];
            var accesId = credentials[1];

            var verifyAccess = ctx.SP_SEGURIDAD_ATTRIBUTE_VALIDAR_APLICACION(clientname, accesId).Select(x => new {
                codeRes = x.codeRes.GetValueOrDefault(),
                messageRes = x.messageRes,
                codAplicacion = x.codAplicacion
            }).FirstOrDefault();

            if (verifyAccess == null) return false;
            if (verifyAccess.codeRes != Convert.ToInt32(HttpStatusCode.OK)) return false;
            AplicationData.codAplicacion = verifyAccess.codAplicacion;
            return true;
        }
    }
}