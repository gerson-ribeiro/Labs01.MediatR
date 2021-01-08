using Labs01.MediatR.ProductContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Persistence
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("LabsMediatR");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
