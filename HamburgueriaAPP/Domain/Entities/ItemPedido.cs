using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }

        public ItemPedido(Produto produto, int quantidade)
        {
            if (produto == null)
            {
                throw new ArgumentNullException("O produto não pode ser nulo.");
            }

            if (!produto.Ativo)
            {
                throw new ArgumentException("O produto deve estar ativo para ser adicionado ao pedido.");
            }

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");


            Id = Guid.NewGuid();
            ProdutoId = produto.Id;
            NomeProduto = produto.Nome;
            PrecoUnitario = produto.Preco;
            Quantidade = quantidade;

        }

        public void AumentantarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade a ser aumentada deve ser maior que zero.");

            Quantidade += quantidade;
        }

        public void DiminuirQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            if (Quantidade - quantidade < 0)
                throw new ArgumentException("A quantidade a ser diminuída não pode resultar em um valor negativo.");

            Quantidade += quantidade;

        }

        public decimal Subtotal()
        {
            return PrecoUnitario * Quantidade;
        }

    }
}
