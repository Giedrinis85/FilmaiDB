using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class SQLAktorius : IAktoriai
    {
        private readonly AppDbContext context;

        public SQLAktorius(AppDbContext context)
        {
            this.context = context;
        }

        public Aktorius Add(Aktorius aktorius)
        {
            context.Aktoriai.Add(aktorius);
            context.SaveChanges();
            return aktorius;
        }

        public Aktorius Delete(int id)
        {
            Aktorius aktorius = context.Aktoriai.Find(id);

            if (aktorius != null)
            {
                context.Aktoriai.Remove(aktorius);
                context.SaveChanges();
            }
            return aktorius;
        }

        public IEnumerable<Aktorius> GetAllAktoriai()
        {
            return context.Aktoriai;
        }

        public Aktorius GetAktorius(int Id)
        {
            return context.Aktoriai.Find(Id);
        }

        public Aktorius Update(Aktorius aktoriusPokyciai)
        {
            var aktorius = context.Aktoriai.Attach(aktoriusPokyciai);
            aktorius.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return aktoriusPokyciai;
        }
    }
}