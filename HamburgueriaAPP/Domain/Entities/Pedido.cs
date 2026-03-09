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

        private readonly List<ItemPedido> _itens = new();

        private readonly List<Pagamento> _pagamentos = new();


        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public StatusPedido Status { get; private set; }
        public Cliente? Cliente { get; private set; }


        public IReadOnlyCollection<ItemPedido> Itens => _itens;
        public IReadOnlyCollection<Pagamento> Pagamentos => _pagamentos;

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
                throw new InvalidOperationException("Não é possível alterar pedido finalizado ou cancelado.");
            }

            var itemExistente = _itens.FirstOrDefault(item => item.ProdutoId == produto.Id);

            // FirstOrDefault retorna o primeiro item que corresponde à condição ou null se nenhum item for encontrado.

            if (itemExistente != null)
            {
                itemExistente.AumentantarQuantidade(quantidade);
            }

            else
            {
                var itemPedido = new ItemPedido(produto, quantidade); // Criar um novo item de pedido com base no produto e na quantidade
                _itens.Add(itemPedido);
            }

        }

        public void RemoverItem(Guid produtoId,  int quantidade)
        {
            if (Status != StatusPedido.Aberto)
            {
                throw new InvalidOperationException("Não é possível remover itens de um pedido que não está aberto.");
            }

            var item = _itens.FirstOrDefault(item => item.ProdutoId == produtoId); // Encontrar o item pelo ID do produto

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
            return _itens.Sum(item  => item.Subtotal());
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

        public void RegistrarPagamento(Pagamento pagamento)
        {
            if (Status != StatusPedido.Aberto)
            {
                throw new InvalidOperationException("Só é possível pagar pedido aberto.");
            }

            else
            {
                decimal restante = ValorRestante();

                pagamento.ConfirmarPagamento(restante);

                _pagamentos.Add(pagamento);


                if (ValorRestante() == 0)
                    FinalizarPedido();

            }
        }

        public decimal TotalPago()
        {
            return _pagamentos.Where(pagamento => pagamento.Status == StatusPagamento.Pago).Sum(pagamento => pagamento.Valor);
                                                  // Para cada pagamento na lista de pagamentos, verifica se o status é "Pago" e, se for, soma o valor do pagamento. O resultado é o total pago para o pedido.
        }

        public decimal ValorRestante()
        {
            return CalcularTotal() - TotalPago();
        }

    }
}

