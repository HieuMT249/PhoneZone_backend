using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using phonezone_backend.Models;

namespace phonezone_backend.Data
{
    public class PhoneZoneDBContext : DbContext
    {
        public PhoneZoneDBContext (DbContextOptions<PhoneZoneDBContext> options)
            : base(options)
        {
        }

        public PhoneZoneDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        public DbSet<phonezone_backend.Models.User> Users { get; set; } = default!;
        public DbSet<phonezone_backend.Models.Product> Products { get; set; } = default!;
        public DbSet<phonezone_backend.Models.ProductDetail> ProductDetails { get; set; } = default!;
    }
}
