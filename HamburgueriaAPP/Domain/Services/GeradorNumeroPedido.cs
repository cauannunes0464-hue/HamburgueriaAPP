using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Services
{
    public class GeradorNumeroPedido
    {
        private int _ultimoNumero = 0;

        public int ProximoNumero()
        {
            _ultimoNumero++;
            return _ultimoNumero;
        }

    }
}
