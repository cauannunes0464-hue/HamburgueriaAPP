using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgueriaAPP.Domain.Enums
{
    public enum StatusPedido // Enum serve para: definir um conjunto de constantes nomeadas, facilitando a legibilidade e manutenção do código. No caso do StatusPedido, ele representa os diferentes estados que um pedido pode ter, como Aberto, Finalizado e Cancelado. Isso torna o código mais claro e fácil de entender, além de reduzir a possibilidade de erros ao usar valores literais.
    {
        Aberto,
        Finalizado,
        Cancelado
    }
}