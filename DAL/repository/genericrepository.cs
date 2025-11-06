using DAL.context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repository
{
    public class genericrepository<Tentity> :igenericrepository<Tentity> where Tentity : class
    {
        protected readonly Restaurantcontext dbcontext;

        public genericrepository(Restaurantcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public List<Tentity> GetAll(Func<IQueryable<Tentity>, IQueryable<Tentity>>? include = null)
        {
            IQueryable<Tentity> query = dbcontext.Set<Tentity>();

            if (include != null)
                query = include(query);

            return query.AsNoTracking().ToList();
        }

        public Tentity? GetById(params object[] keys)
        {
            return dbcontext.Set<Tentity>().Find(keys);
        }
        
        public void Add(Tentity entity)
        {
            dbcontext.Set<Tentity>().Add(entity);
        }
        public void Update(Tentity entity)
        {
            dbcontext.Set<Tentity>().Update(entity);
        }
        public void Delete(Tentity entity)
        {
            dbcontext.Set<Tentity>().Remove(entity);
        }

    }
}
    

