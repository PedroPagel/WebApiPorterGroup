using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.AreaPredial
{
    public class Bloco
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255), MinLength(1)]
        public string Nome { get; set; }

        public Condominio Condominio { get; set; }

        [Required]
        public int CondominioId { get; set; }

        public ICollection<Apartamento> Apartamentos { get; set; }
    }
}
