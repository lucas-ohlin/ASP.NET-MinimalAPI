namespace Labb1_MinimalAPI.Services {
    public interface IRepository<T> {

        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetByTitle(string title);
        public Task<T> GetById(Guid id);
        public Task<T> Create(T obj);
        public Task<T> Update(T obj);
        public Task<T> Delete(Guid id);

    }
}
