using FilmaiDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class AktoriusDetailsViewModel
    {
        public Aktorius Aktorius { get; set; }
        public string DB_Filmai { get; set; }
        public string PageTitle { get; set; }
        public int FilmuSkaicius { get; set; }
        
    }
}
