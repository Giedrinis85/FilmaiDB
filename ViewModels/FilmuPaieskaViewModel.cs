using FilmaiDB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class FilmuPaieskaViewModel
    {
        public List<Filmas> Filmai { get; set; }

        //public string SearchString { get; set; }

        public SelectList ZanraiList { get; set; }
        public SelectList MetuList { get; set; }
        public string FilmoZanras { get; set; }
        public int FilmoMetai { get; set; }
        public string SearchBy { get; set; }
        public string Search { get; set; }

        //public string DB_Aktoriai { get; set; }
    }
}
