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

        public DbSet<phonezone_backend.Models.User> Users { get; set; } = default!;
    }
}
