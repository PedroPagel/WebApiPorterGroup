using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.AreaPredial
{
    public class Condominio
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string EmailSindico { get; set; }

        [Required, StringLength(12), MinLength(11)]
        public string TelefoneSindico { get; set; }

        [Required, StringLength(255), MinLength(1)]
        public string Nome { get; set; }

        public ICollection<Bloco> Blocos { get; set; }

        public ICollection<Apartamento> Apartamentos { get; set; }
    }
}
