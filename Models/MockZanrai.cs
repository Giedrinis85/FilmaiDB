using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class MockZanrai : IZanrai
    {
        private readonly List<Zanrai> _zanruList;
        
        public MockZanrai()
        {
            _zanruList = new List<Zanrai>()
            {
                new Zanrai() { Id = 1, Pavadinimas = "Drama" },
                new Zanrai() { Id = 2, Pavadinimas = "Komedija" },
                new Zanrai() { Id = 3, Pavadinimas = "Veiksmo" },
            };
        }

        public Zanrai Add(Zanrai zanrai)
        {
            zanrai.Id = _zanruList.Max(e => e.Id) + 1;
            _zanruList.Add(zanrai);
            return zanrai;
        }

        public Zanrai Delete(int id)
        {
            Zanrai zanrai = _zanruList.FirstOrDefault(e => e.Id == id);

            if (zanrai != null)
            {
                _zanruList.Remove(zanrai);
            }

            return zanrai;
        }

        public IEnumerable<Zanrai> GetAllZanrai()
        {
            return _zanruList;
        }

        public Zanrai GetZanrai(int Id)
        {
            return _zanruList.FirstOrDefault(e => e.Id == Id);
        }

        public Zanrai Update(Zanrai zanraiPokyciai)
        {
            Zanrai zanrai = _zanruList.FirstOrDefault(e => e.Id == zanraiPokyciai.Id);

            if (zanrai != null)
            {
                zanrai.Pavadinimas = zanraiPokyciai.Pavadinimas;
            }

            return zanrai;
        }
    }
}
