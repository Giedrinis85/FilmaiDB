using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public interface IAktoriai
    {
        Aktorius GetAktorius(int id);
        IEnumerable<Aktorius> GetAllAktoriai();
        Aktorius Add(Aktorius aktorius);
        Aktorius Update(Aktorius aktoriusPokyciai);
        Aktorius Delete(int id);
    }
}
