using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace ApiEasyWork.Util
{
    public class HelperToken
    {
        public static ClaimTokenData LeerToken(IPrincipal principal)
        {
            var response = new ClaimTokenData();
            try
            {
                ClaimsPrincipal claimsToken = principal as ClaimsPrincipal;
                var clames = claimsToken.Claims.ToList();
                foreach (var item in clames)
                {
                    if (item.Type == "cod_aplicacion")
                    {
                        response.cod_aplicacion = item.Value;
                    }
                    if (item.Type == "user_code")
                    {
                        response.cod_usuario = item.Value;
                    }
                }
                response.codigo = 1;
            }
            catch (Exception)
            {
                response.codigo = 0;
            }
            return response;
        }
        public static bool validClienteCodAplicativo(ClaimTokenData datos)
        {
            if (String.IsNullOrEmpty(datos.cod_aplicacion))
            {
                return false;
            }
            return true;
        }
    }
}