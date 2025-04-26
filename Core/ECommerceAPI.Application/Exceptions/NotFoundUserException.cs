using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanıcı veya şifre hatalı")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }
    }
}
