using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        protected readonly TMContext _db;

        public BaseService(TMContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task CreateAsync(T model, bool saved = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _db.Set<T>().Add(model);
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().Where(m => !m.IsRemove).AsNoTracking();
        }

        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return GetAll().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllOrder().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllOrder(bool asc = true)
        {
            var datas = GetAll();
            datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
            return datas;
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await GetAll().FirstAsync(m => m.Id == id);
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            await RemoveAsync(model.Id, saved);
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var m = await GetOneByIdAsync(id);
            m.IsRemove = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> SaveChange()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}
