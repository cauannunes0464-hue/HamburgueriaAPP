using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public string NomeProduto { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }

        public ItemPedido(string nomedeProduto, decimal precoUnitario, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");


            Id = Guid.NewGuid();
            NomeProduto = nomedeProduto;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;

        }

        public decimal Subtotal()
        {
            return PrecoUnitario * Quantidade;
        }

    }
}
