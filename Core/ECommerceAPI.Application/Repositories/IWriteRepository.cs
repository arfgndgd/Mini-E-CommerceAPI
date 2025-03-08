using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity // T bir class olabilir diye belirttik
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas); //birden fazla veri
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id); //firstordefaultAsync id'li olduğu için asenkron Task<> dönüş tipi olacak
        bool Update(T model);
        Task<int> SaveAsync();

    }
}
