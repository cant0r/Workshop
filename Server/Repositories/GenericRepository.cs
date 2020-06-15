using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity ent, params object[] children)
        {
            using var ctx = new WorkshopContext();
          
            ctx.Set<TEntity>().Add(ent);
            foreach (object o in children)
            {
                if (o != null)
                    ctx.Entry(o).State = EntityState.Modified;
            }
            ctx.SaveChanges();
        }
        
        public void AddAll(IEnumerable<TEntity> ent)
        {
            using var ctx = new WorkshopContext();
            ctx.Set<TEntity>().AddRange(ent);
            ctx.SaveChanges();
        }

        public TEntity Get(long id)
        {
            using var ctx = new WorkshopContext();
            return ctx.Set<TEntity>().Find(id);
        }
        public TEntity Get<T>(T id) where T : class
        {
            using var ctx = new WorkshopContext();
            return ctx.Set<TEntity>().Single(t => t.Equals(id));
        }
        public IEnumerable<TEntity> GetAll()
        {
            using var ctx = new WorkshopContext();
            return ctx.Set<TEntity>().ToList();
        }

        public void Remove(TEntity ent)
        {
            using var ctx = new WorkshopContext();
            ctx.Set<TEntity>().Remove(ent);
            ctx.SaveChanges();
        }
        public void RemoveAll(IEnumerable<TEntity> ent)
        {
            using var ctx = new WorkshopContext();
            ctx.Set<TEntity>().RemoveRange(ent);
            ctx.SaveChanges();
        }
        public void Update(TEntity ent)
        {
            using var ctx = new WorkshopContext();
            ctx.Set<TEntity>().Update(ent);
            ctx.SaveChanges();

        }
        public void UpdateAll(IEnumerable<TEntity> ent)
        {
            using var ctx = new WorkshopContext();
            ctx.Set<TEntity>().UpdateRange(ent);
            ctx.SaveChanges();
        }
    }
}
