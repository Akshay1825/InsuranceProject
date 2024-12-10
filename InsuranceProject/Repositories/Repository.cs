using InsuranceProject.Data;
using InsuranceProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InsuranceProject.Repositories
{
    public class Repository<T>:IRepository<T> where T : BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<T> _table;

        public Repository(Context context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _table.Update(entity);
            return _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            var entity = _table.Find(id);
            return entity != null && !entity.IsDeleted ? entity : null;
        }
        public IQueryable<T> GetAll()
        {
            return _table.Where(e => !e.IsDeleted).AsQueryable();
        }

        public void Update(T entity)
        {
            if (entity.IsDeleted)
            {
                throw new InvalidOperationException("Cannot update a soft-deleted entity");
            }
            _table.Update(entity);
            _context.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _table.Any(predicate);
        }
    }
}
