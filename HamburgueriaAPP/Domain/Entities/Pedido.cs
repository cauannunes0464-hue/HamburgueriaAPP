using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using HamburgueriaAPP.Domain.Enums;

namespace HamburgueriaAPP.Domain.Entities
{
    public class Pedido
    {

        private readonly List<ItemPedido> _itens;

        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public StatusPedido Status { get; private set; }
        public Cliente? Cliente { get; private set; }

        public IReadOnlyCollection<ItemPedido> Itens => _itens;


        public Pedido(int numero, Cliente? cliente = null)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            DataCriacao = DateTime.Now;
            Status = StatusPedido.Aberto;
            Cliente = cliente;
        }

        public void AdicionarItem(ItemPedido item)
        {
            if(Status != StatusPedido.Aberto) 
            { 
                throw new InvalidOperationException("Não é possível adicionar itens a um pedido que não está aberto.");
            }

            _itens.Add(item);
        }

        public decimal CalcularTotal()
        {
            return _itens.Sum(p  => p.Subtotal());
        }

        public void FinalizarPedido()
        {
            if (!_itens.Any())
            {
                throw new InvalidOperationException("Não é possível finalizar um pedido sem itens.");
            } 

            Status = StatusPedido.Finalizado;
        }

    }
}
