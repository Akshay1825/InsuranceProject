using InsuranceProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InsuranceProject.Repositories
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _table;

        public Repository(Context context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return _table.Find(id);
        }

        public void Add(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

    }
}
