﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.ViewModels.Products
{
    // CQRS Handlerdan sonra artık kullanılmıyor 
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
