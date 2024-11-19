using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Projeto.Constants;
using System.Security.Claims;

namespace Projeto.Controllers
{
    public static class ControllerBaseExtention
    {
        public static ObjectResult InternalServerError(this ControllerBase controller, Exception ex)
        {
            return new ObjectResult(new RetornoErro(new Errors(new List<string>
                    {
                        ex.Message
                    })))
            {
                StatusCode = 500
            };
        }

        /// <summary>
        /// Retorna o users_id do usuário informado no token
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>users_id inforamdo no token</returns>
        public static int? Users_id(this ControllerBase controller)
        {
            string claimValue = Get_claim(controller, LoginConstants.users_id);
            if (int.TryParse(claimValue, out int value))
                return value;
            return null;
        }
        /// <summary>
        /// Retorna o Grupo do usuário informado no token
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Grupo</returns>
        public static string? Grupo(this ControllerBase controller)
        {
            return Get_claim(controller, ClaimTypes.Role);
        }
        public static string? GetTokenLdap(this ControllerBase controller)
        {
            return Get_claim(controller, LoginConstants.Ldap);
        }
        /// <summary>
        /// Retorna o valor da claim informada no token
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key">Claim</param>
        /// <returns>Valor associado na claim, quando estiver disponível. Se não for encontrada retona null</returns>
        private static string? Get_claim(this ControllerBase controller, string key)
        {
            var identity = controller.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return identity.Claims.FirstOrDefault(c => c.Type == key)?.Value;
            }
            return null;
        }
        internal class RetornoErro
        {
            public Errors errors { get; set; }
            public RetornoErro(Errors errors)
            {
                this.errors = errors;
            }
        }
        internal class Errors
        {
            public List<string> erros { get; set; }
            public Errors(List<string> erros)
            {
                this.erros = erros;
            }
        }
    }
}
