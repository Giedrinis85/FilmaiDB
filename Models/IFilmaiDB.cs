using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public interface IFilmaiDB
    {
        Filmas GetFilmas(int Id);
        IEnumerable<Filmas> GetAllFilmas();
        Filmas Add(Filmas filmas);
        Filmas Update(Filmas filmasPokyciai);
        Filmas Delete(int id);
    }
}
