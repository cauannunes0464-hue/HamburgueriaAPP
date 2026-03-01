using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Cliente(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;

        }
        public override string ToString()
        {
            return $"{Nome} - {Telefone} - {Email}";
        }

    }
}
