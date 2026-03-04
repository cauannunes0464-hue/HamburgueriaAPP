using HamburgueriaAPP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public decimal Valor { get; private set; }
        public FormaPagamento Forma { get; private set; }
        public StatusPagamento Status { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public decimal Troco { get; private set; }


        public Pagamento (decimal valor, FormaPagamento forma)
        {

            if (valor <= 0)
                throw new ArgumentException("O valor do pagamento deve ser maior que zero.");


            Id = Guid.NewGuid();
            Valor = valor;
            Forma = forma;
            Status = StatusPagamento.Pendente;
            DataPagamento = DateTime.Now;

        }

        public void ConfirmarPagamento(decimal valorNecessario)
        {

            if (Status != StatusPagamento.Pendente)
            {
                throw new InvalidOperationException("Pagamento já foi processado.");
            }


            if (Forma == FormaPagamento.Dinheiro)
            {
                if (Valor < valorNecessario)
                {
                    throw new InvalidOperationException("Valor em dinheiro insuficiente para o pagamento.");
                }

                else
                {
                    Troco = Valor - valorNecessario;
                }

            }

            else
            {
                if (Valor > valorNecessario)
                {
                    throw new InvalidOperationException("Pagamento maior que o valor restante.");
                }

                if (Valor < 0)
                {
                    throw new ArgumentException("Valor inativo.");
                }

            }

            Status = StatusPagamento.Pago;
            DataPagamento = DateTime.Now;

        }
    }
}
