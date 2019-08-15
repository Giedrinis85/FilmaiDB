using FilmaiDB.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class FilmoSukurimasViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pavadinimas privalomas.")]
        public string Pavadinimas { get; set; }

        [Required(ErrorMessage = "Išleidimo metai privalomi.")]
        [Range(1900, 2050, ErrorMessage = "Neteisingi metai. Itervalas: 1900-2050.")]
        public int? IsleidimoData { get; set; }

        //[Required(ErrorMessage = "Žanras yra privalomas.")]
        //public Zanras? Zanras { get; set; }
        //[Required(ErrorMessage = "Aktoriai privalomi.")]
        //public string Aktoriai { get; set; }

        [Required(ErrorMessage = "Žanras privalomas.")]
        public int? ZanraiId { get; set; }

        public Zanrai Zanrai { get; set; }

        //[Required(ErrorMessage = "Aktoriai yra privalomi.")]
        //public Aktorius DB_Aktoriai { get; set; }

        [Required(ErrorMessage = "Privaloma pasirinkti vieną ar kelis aktorius.")]
        public IEnumerable<int> SelectedVal { get; set; }
    }
}
