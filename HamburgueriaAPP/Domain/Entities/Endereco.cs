using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace HamburgueriaAPP.Domain.Entities
{
    public class Endereco
    {
        public Guid Id { get; private set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string CEP { get; private set; }

        public Endereco(string rua, string numero, string bairro, string cidade, string cep)
        {
            if (string.IsNullOrWhiteSpace(rua))
                throw new ArgumentException("Rua é obrigatória.");

            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Número é obrigatório.");

            if (string.IsNullOrWhiteSpace(bairro))
                throw new ArgumentException("Bairro é obrigatório.");

            if (string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Cidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP é obrigatório.");

            Id = Guid.NewGuid();
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            CEP = cep;

        }

        public override string ToString()
        {
            return $"{Rua}, {Numero} - {Bairro}, {Cidade} - CEP: {CEP}";
        }

    }
}
