using ProjetoSistemasWeb.Domain.Entities;
using ProjetoSistemasWeb.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Aplication
{
    public class UsuarioApplication
    {
        IUsuarioRepository usuarioRepository;

        public UsuarioApplication(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public bool Autenticar(string userName, string password)
        {
            var user = this.usuarioRepository.Find(userName.ToLower());
            if (user == null)
            {
                throw new ApplicationException("Usuario não encontrado");
            }

            if (!user.PassswordIsValid(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void TrocarSenha(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("E-mail deve ser informado");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ApplicationException("Nova senha deve ser informada");
            }

            var user = this.usuarioRepository.Find(email);
            user.Password = newPassword;

            this.usuarioRepository.Update(user);
        }
    }
}
