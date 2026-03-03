using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using HamburgueriaAPP.Domain.Enums;
using HamburgueriaAPP.Domain.Services;

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

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if(Status != StatusPedido.Aberto) 
            { 
                throw new InvalidOperationException("git .");
            }

            var itemExistente = _itens.FirstOrDefault( _itens => _itens.ProdutoId == produto.Id);

            if(itemExistente != null)
            {
                itemExistente.AumentantarQuantidade(quantidade);
            }

            else
            {
                var itemPedido = new ItemPedido(produto, quantidade);
                _itens.Add(itemPedido);
            }

        }

        public void RemoverItem(Guid produtoId,  int quantidade)
        {
            if (Status != StatusPedido.Aberto)
            {
                throw new InvalidOperationException("Não é possível remover itens de um pedido que não está aberto.");
            }

            var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId); // Encontrar o item pelo ID do produto

            if (item == null)
            {
                throw new InvalidOperationException("Produto não encontrado no pedido.");
            }

            item.DiminuirQuantidade(quantidade);

            if (item.Quantidade == 0)
            {
                _itens.Remove(item);
            }
        }

        public decimal CalcularTotal()
        {
            return _itens.Sum(p  => p.Subtotal());
        }

        public void FinalizarPedido()
        {
            if (Status != StatusPedido.Aberto)
                throw new InvalidOperationException("Pedido já foi finalizado ou cancelado.");

            if (!_itens.Any())
                throw new InvalidOperationException("Não é possível finalizar um pedido sem itens."); 

            Status = StatusPedido.Finalizado;
        }

        public void Cancelar()
        {
            if (Status == StatusPedido.Finalizado)
                throw new InvalidOperationException("Pedido finalizado não pode ser cancelado.");

            if (Status == StatusPedido.Cancelado)
                throw new InvalidOperationException("Pedido já está cancelado.");

            Status = StatusPedido.Cancelado;
        }



    }
}

// git commit -m "Implementação da classe Pedido com funcionalidades de adicionar, remover itens, calcular total, finalizar e cancelar pedidos.