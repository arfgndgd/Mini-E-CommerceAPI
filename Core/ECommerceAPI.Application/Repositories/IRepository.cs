using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity // T bir class olabilir diye belirttik 
                                                         // BaseEntity olarak da belirtilebilir
    {
        DbSet<T> Table { get; } //veritabanındaki tablea denk gelir. dbset table alır set işlemi yapmaz
    }
}
