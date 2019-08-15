using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class Filmas
    {
        public Filmas()
        {
            this.DB_Aktoriai = new List<Aktorius>();
            this.AktoriaiFilmai = new List<AktoriaiFilmas>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Pavadinimas privalomas.")]
        public string Pavadinimas { get; set; }

        [Required(ErrorMessage = "Išleidimo metai privalomi.")]
        [Range(1900, 2050, ErrorMessage = "Neteisingi metai. Itervalas: 1900-2050.")]
        public int? IsleidimoData { get; set; }

        //[Required(ErrorMessage = "Žanras privalomas.")]
        //public Zanras Zanras { get; set; }

        [Required(ErrorMessage = "Žanras privalomas.")]
        public int ZanraiId { get; set; }

        public virtual Zanrai Zanrai { get; set; }

        //[Required(ErrorMessage = "Aktoriai privalomi.")]
        //public string Aktoriai { get; set; }

        [NotMapped]
        public virtual ICollection<Aktorius> DB_Aktoriai { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Privaloma pasirinkti vieną ar kelis aktorius.")]
        //public IEnumerable<int> SelectedVal { get; set; }

        public virtual ICollection<AktoriaiFilmas> AktoriaiFilmai { get; set; }        
    }
}
