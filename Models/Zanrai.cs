using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class Zanrai
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Žanro pavadinimas privalomas.")]
        public string Pavadinimas { get; set; }

        ICollection<Filmas> Filmai { get; set; }
    }
}
