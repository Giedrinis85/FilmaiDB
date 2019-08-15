using FilmaiDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.ViewModels
{
    public class ZanraiIstrynimasViewModel
    {
        public Zanrai Zanrai { get; set; }
        public List<Filmas> Filmai { get; set; }
        public string PageTitle { get; set; }
    }
}
