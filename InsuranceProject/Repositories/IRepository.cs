namespace InsuranceProject.Repositories
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();
        public T GetById(Guid id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
