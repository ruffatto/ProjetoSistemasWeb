using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjetoSistemasWeb.Aplication;
using ProjetoSistemasWeb.Repository;
using ProjetoSistemasWeb.Domain.Repository;

namespace WebApi.App_Start
{   
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {
        private UsuarioApplication usuarioApplication;
        private IUsuarioRepository usuarioRepository;

        public AccessTokenProvider()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            usuarioRepository = new UsuarioRepository(connectionString);
            usuarioApplication = new UsuarioApplication(usuarioRepository);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (usuarioApplication.Autenticar(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Acesso Inválido", "Usuario ou senha são inválidos");
                return;
            }
        }
    }
}