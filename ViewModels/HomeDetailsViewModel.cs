using FilmaiDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Filmas Filmas { get; set; }

        //public Zanrai Zanrai { get; set; }

        public string DB_Aktoriai { get; set; }
        public string PageTitle { get; set; }
    }
}
