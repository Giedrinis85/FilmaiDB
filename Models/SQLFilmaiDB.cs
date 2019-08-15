using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class SQLFilmaiDB : IFilmaiDB
    {
        private readonly AppDbContext context;

        public SQLFilmaiDB(AppDbContext context)
        {
            this.context = context;
        }

        public Filmas Add(Filmas filmas)
        {
            context.Filmai.Add(filmas);
            context.SaveChanges();
            return filmas;
        }

        public Filmas Delete(int id)
        {
            Filmas filmas = context.Filmai.Find(id);

            if(filmas != null)
            {
                context.Filmai.Remove(filmas);
                context.SaveChanges();
            }
            return filmas;
        }

        public IEnumerable<Filmas> GetAllFilmas()
        {
             return context.Filmai;
        }

        public Filmas GetFilmas(int Id)
        {
            return context.Filmai.Find(Id);
        }

        public Filmas Update(Filmas filmasPokyciai)
        {
            var filmas = context.Filmai.Attach(filmasPokyciai);
            filmas.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return filmasPokyciai;
        }
    }
}
