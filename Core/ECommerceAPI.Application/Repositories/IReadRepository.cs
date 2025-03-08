using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity // T bir class olabilir diye belirttik
    {
        // sorgu ise IQueryable, çoğuldur
        // inmemoryde çalışma yapılacak ise INumarable (List örn)

        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); // where şartı gibi kullanılacak
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true); //firstordefault async await
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
