using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Args { get; set; }
        public bool Active { get; set; }
    }
}
