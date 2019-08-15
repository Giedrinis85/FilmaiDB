using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class Aktorius
    {
        public Aktorius()
        {
            this.Filmai = new List<Filmas>();
            this.AktoriaiFilmai = new List<AktoriaiFilmas>();
        }
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Vardas ir/arba pavardė privalomi.")]
        [Display(Name = "Aktorius")]
        public string VardasPavarde { get; set; }

        [NotMapped]
        public virtual ICollection<Filmas> Filmai { get; set; }        
        public virtual ICollection<AktoriaiFilmas> AktoriaiFilmai { get; set; }        
    }
}
