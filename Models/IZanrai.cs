using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public interface IZanrai
    {
        Zanrai GetZanrai(int Id);
        IEnumerable<Zanrai> GetAllZanrai();
        Zanrai Add(Zanrai zanrai);
        Zanrai Update(Zanrai zanraiPokyciai);
        Zanrai Delete(int id);
    }
}
