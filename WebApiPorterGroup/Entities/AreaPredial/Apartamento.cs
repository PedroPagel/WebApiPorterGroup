using Entities.Pessoa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.AreaPredial
{
    public class Apartamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int Andar { get; set; }

        public Bloco Bloco { get; set; }

        [Required]
        public int BlocoId { get; set; }

        public Condominio Condominio { get; set; }

        [Required]
        public int CondominioId { get; set; }

        public ICollection<Morador> Moradores { get; set; }
    }
}
