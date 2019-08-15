using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class MockFilmaiDB : IFilmaiDB
    {
        private readonly List<Filmas> _filmuList;

        public MockFilmaiDB()
        {
            _filmuList = new List<Filmas>()
            {
                new Filmas() { Id = 1, Pavadinimas = "Filmas1", IsleidimoData = 2011 }, //Zanras = Zanras.Drama,
                    //Aktoriai = "Aktorius11, Aktorius12" },
                new Filmas() { Id = 2, Pavadinimas = "Filmas2", IsleidimoData = 2012 }, //Zanras = Zanras.Komedija,
                    //Aktoriai = "Aktorius21, Aktorius22" },
                new Filmas() { Id = 3, Pavadinimas = "Filmas3", IsleidimoData = 2013 }, //Zanras = Zanras.Veiksmo,
                    //Aktoriai = "Aktorius31, Aktorius32" },
            };
        }

        public Filmas Add(Filmas filmas)
        {
            filmas.Id =_filmuList.Max(e => e.Id) + 1;
            _filmuList.Add(filmas);
            return filmas;
        }

        public Filmas Delete(int id)
        {
            Filmas filmas = _filmuList.FirstOrDefault(e => e.Id == id);

            if(filmas != null)
            {
                _filmuList.Remove(filmas);
            }

            return filmas;
        }

        public IEnumerable<Filmas> GetAllFilmas()
        {
            return _filmuList;
        }

        public Filmas GetFilmas(int Id)
        {
            return _filmuList.FirstOrDefault(e => e.Id == Id);
        }

        public Filmas Update(Filmas filmasPokyciai)
        {
            Filmas filmas = _filmuList.FirstOrDefault(e => e.Id == filmasPokyciai.Id);

            if (filmas != null)
            {
                filmas.Pavadinimas = filmasPokyciai.Pavadinimas;
                filmas.IsleidimoData = filmasPokyciai.IsleidimoData;
                //filmas.Zanras = filmasPokyciai.Zanras;
                //filmas.Aktoriai = filmasPokyciai.Aktoriai;
            }

            return filmas;
        }
    }
}
