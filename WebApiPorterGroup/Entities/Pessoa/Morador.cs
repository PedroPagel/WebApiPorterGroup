using Entities.AreaPredial;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Pessoa
{
    public class Morador
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }

        [Required, StringLength(255), MinLength(1)]
        public string Nome { get; set; }

        [Required, StringLength(12), MinLength(11)]
        public string Telefone { get; set; }

        [Required, StringLength(11), MinLength(11)]
        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public Apartamento Apartamento { get; set; }

        [Required]
        public int ApartamentoId { get; set; }
    }
}
