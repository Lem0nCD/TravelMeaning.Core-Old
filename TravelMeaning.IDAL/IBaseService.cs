using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IDAL
{
    public interface IBaseService<T> : IDisposable where T : BaseEntity
    {
        Task CreateAsync(T model, bool saved = true);
        Task EditAsync(T model, bool saved = true);
        Task RemoveAsync(T model, bool saved = true);
        Task RemoveAsync(Guid id, bool saved = true);
        Task<bool> SaveChange();
        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllOrder(bool asc = true);
        IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0);
        IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true);
    }
}
