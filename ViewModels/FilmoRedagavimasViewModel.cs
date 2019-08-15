using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class FilmoRedagavimasViewModel : FilmoSukurimasViewModel
    {
        //public int Id { get; set; }

        public int SenasId { get; set; }

        [Display(Name = "Aktoriai")]
        public MultiSelectList MultiSel_Aktoriai { get; set; }
    }
}
