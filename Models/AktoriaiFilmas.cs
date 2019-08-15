using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class AktoriaiFilmas
    {
        public int Id { get; set; }

        public int AktoriusId { get; set; }
        public Aktorius Aktorius { get; set; }

        public int FilmasId { get; set; }
        public Filmas Filmas { get; set; }
    }
}
