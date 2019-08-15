using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class AktoriusRedagavimasViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Aktorius")]
        public string VardasPavarde { get; set; }

        public ICollection<int> SelectedFilmai { get; set; }
    }
}
