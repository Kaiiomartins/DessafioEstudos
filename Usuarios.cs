using System;

namespace UseriosTeste
{
    public class Usuarios
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Usuarios(string nome, string email)
        {
            this.Nome = nome;
            this.Email = email;
        }
    }
}
