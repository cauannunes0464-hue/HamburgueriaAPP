using HamburgueriaAPP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public CategoriaProduto Categoria { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }


        public Produto(string nome, decimal preco, CategoriaProduto categoria)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");


            Id = Guid.NewGuid();
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
            Ativo = true;
            DataCadastro = DateTime.Now;

        }


        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0)
            {
                throw new ArgumentException("Preço deve ser maior que zero.");                
            }

            Preco = novoPreco;

        }

        public void DesativarProduto()
        {
            Ativo = false;
        }

        public override string ToString()
        { 
            return $"Produto: {Nome}, Preço: {Preco:F2}";
        }

    }
}
