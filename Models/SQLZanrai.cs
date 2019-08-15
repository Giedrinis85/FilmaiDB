using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class SQLZanrai : IZanrai
    {
        private readonly AppDbContext context;

        public SQLZanrai(AppDbContext context)
        {
            this.context = context;
        }

        public Zanrai Add(Zanrai zanrai)
        {
            context.Zanrai.Add(zanrai);
            context.SaveChanges();
            return zanrai;
        }

        public Zanrai Delete(int id)
        {
            Zanrai zanrai = context.Zanrai.Find(id);

            if (zanrai != null)
            {
                context.Zanrai.Remove(zanrai);
                context.SaveChanges();
            }
            return zanrai;
        }

        public IEnumerable<Zanrai> GetAllZanrai()
        {
            return context.Zanrai;
        }

        public Zanrai GetZanrai(int Id)
        {
            return context.Zanrai.Find(Id);
        }

        public Zanrai Update(Zanrai zanraiPokyciai)
        {
            var zanrai = context.Zanrai.Attach(zanraiPokyciai);
            zanrai.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return zanraiPokyciai;
        }
    }
}
