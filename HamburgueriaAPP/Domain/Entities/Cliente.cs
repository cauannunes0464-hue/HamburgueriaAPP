using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class Cliente
    {

        private readonly List<Endereco> _enderecos = new();

        public Guid Id { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }


        public IReadOnlyCollection<Endereco> Enderecos => _enderecos;

        public Cliente(string nome, string telefone, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("Telefone é obrigatório.");


            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Ativo = true;

        }

        public void AtualizarDados(string nome, string telefone, string email)
        {

            if (!Ativo)
            { 
                throw new ArgumentException("Não é possível atualizar um cliente inativo."); 
            }

            Nome = nome;
            Telefone = telefone;
            Email = email;

        }


        public void AdicionarEndereco(Endereco endereco)
        {
            if (!Ativo)
            {
                throw new ArgumentException("Não é possível adicionar um endereço a um cliente inativo.");
            }

            _enderecos.Add(endereco);
        }


        public void Desativar()
        {
            Ativo = false;
        }


        public override string ToString()
        {
            return $"{Nome} - {Telefone} - {Email}";
        }

    }
}
