using System;
using System.Collections.Generic;
using System.Text;
using HamburgueriaAPP.Application;
using HamburgueriaAPP.Domain.Entities;
using HamburgueriaAPP.ViewModels;

namespace HamburgueriaAPP.UI.ViewModels
{
    public class PedidoViewModel : ViewModelBase
    {

        private readonly PedidoService _pedidoService; 

        private Pedido? _pedidoAtual;
        public Pedido? PedidoAtual
        {
            get => _pedidoAtual;
            set => SetProperty(ref _pedidoAtual, value); 
        }


        public decimal Total
        {
            get
            {
                if (PedidoAtual == null)
                {
                    return 0;
                }

                return PedidoAtual.CalcularTotal();

            }
        }

        public PedidoViewModel(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public void CriarPedido()
        {
            PedidoAtual = _pedidoService.CriarPedido();

            OnPropertyChanged(nameof(Total));

        }


    }
}

// commit message: "Implementação inicial do PedidoViewModel, incluindo criação de pedidos e cálculo do total e PedidoService para gerenciar os pedidos. Adicionada propriedade PedidoAtual e método CriarPedido."