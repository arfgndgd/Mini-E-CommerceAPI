using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        T.Token CreateAccessToken(int munite);
    }
}
