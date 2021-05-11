using System;

namespace Contracts.Request
{
    public class MoradorRequest
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdApartamento { get; set; }
    }
}
