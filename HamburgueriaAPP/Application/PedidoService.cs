using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HamburgueriaAPP.Domain.Entities;
using HamburgueriaAPP.Domain.Services;

namespace HamburgueriaAPP.Application
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidos = new();
        private readonly GeradorNumeroPedido _geradorNumeroPedido; 

        public PedidoService (GeradorNumeroPedido geradorNumeroPedido)
        {
            _geradorNumeroPedido = geradorNumeroPedido;
        }

        public Pedido CriarPedido()
        {
            int numero = _geradorNumeroPedido.ProximoNumero();

            var pedido = new Pedido(numero);

            _pedidos.Add(pedido);
          
            return pedido;

        }

        public Pedido? ObterPedido(int numero)
        {
            return _pedidos.FirstOrDefault(pedido => pedido.Numero == numero);
        }

       
        public void AdcionarItens(int numeroPedido, Produto produto, int quantidade)
        {
            var pedido = ObterPedido(numeroPedido);

            if (pedido == null)
            {
                throw new ValidationException($"Pedido com número {numeroPedido} não encontrado.");
            }

            else
            {
                pedido.AdicionarItem(produto, quantidade);
            }
 
        }

        public void RegistrarPagamentos(int numeroPedido, Pagamento pagamento)
        {
            var pedido = ObterPedido(numeroPedido);

            if (pedido == null)
            {
                throw new ValidationException("Pedido com número {numeroPedido} não encontrado.");
            }

            else
            {
                pedido.RegistrarPagamento(pagamento);
            }

        }

        public IReadOnlyCollection<Pedido> ListarPedidos()
        {
            return _pedidos;
        }

    }
}
