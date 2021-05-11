using System;

namespace Contracts.Result
{
    public class MoradorResult
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdApartamento { get; set; }
    }
}
