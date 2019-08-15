using FilmaiDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class ZanruSukurimasViewModel
    {
        [Required(ErrorMessage = "Žanro pavadinimas privalomas.")]
        public string Pavadinimas { get; set; }
    }
}
