using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IDAL
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<bool> CreateAsync(T model, bool saved = true);
        Task<bool> EditAsync(T model, bool saved = true);
        Task<bool> RemoveAsync(T model, bool saved = true);
        Task<bool> RemoveAsync(Guid id, bool saved = true);
        Task<bool> SaveChange();
        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllOrder(bool asc = true);
        IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0);
        IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true);
    }
}
