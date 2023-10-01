using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BookingManagementDbContext _context;

        public Repository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var entityToDelete = _context.Set<T>().Find(guid);
                if (entityToDelete == null)
                {
                    return false; // Data tidak ditemukan, penghapusan gagal.
                }

                _context.Set<T>().Remove(entityToDelete);
                _context.SaveChanges();
                return true; // Penghapusan berhasil.
            }
            catch (Exception ex)
            {
                // Di sini Anda dapat menangani kesalahan penghapusan dengan lebih spesifik.
                return false; // Penghapusan gagal.
            }
        }


        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(Guid guid)
        {
            return _context.Set<T>().Find(guid);
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
