using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.Repos
{
    public class BaseRepo<T>
    {
        protected readonly AppDbContext _appDbContext;

        public BaseRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T item)
        {
            _appDbContext.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            _appDbContext.Remove(item);
            _appDbContext.SaveChanges();
        }
    }
}

