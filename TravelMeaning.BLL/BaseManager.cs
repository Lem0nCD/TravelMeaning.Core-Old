using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class BaseManager<T> : IBaseManager<T> where T : BaseEntity, new()
    {
        public readonly IBaseService<T> _baseSvc;

        public BaseManager(IBaseService<T> baseSvc)
        {
            _baseSvc = baseSvc ?? throw new ArgumentNullException(nameof(baseSvc));
        }

        public async Task CreateAsync(T model, bool saved = true)
        {
           await _baseSvc.CreateAsync(model, saved);
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            await _baseSvc.EditAsync(model, saved);
        }

        public IQueryable<T> GetAll()
        {
            return _baseSvc.GetAll();
        }

        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return _baseSvc.GetAllByPage(pageSize, pageIndex);
        }

        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return _baseSvc.GetAllByPageOrder(pageSize, pageIndex,asc);
        }

        public IQueryable<T> GetAllOrder(bool asc = true)
        {
            return _baseSvc.GetAllOrder(asc);
        }

        public Task<T> GetOneByIdAsync(Guid id)
        {
            return _baseSvc.GetOneByIdAsync(id);
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await _baseSvc.RemoveAsync(model,saved);
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            await _baseSvc.RemoveAsync(id, saved);
        }

        public async Task<bool> SaveChange()
        {
            return await _baseSvc.SaveChange();
        }
    }
}
