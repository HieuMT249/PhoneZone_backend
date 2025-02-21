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

            modelBuilder.Entity<WishList>()
                .HasOne(w => w.User)
                .WithMany(u => u.WishLists)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<OrderCoupon>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCoupons)
                .HasForeignKey(oc => oc.OrderId);

            modelBuilder.Entity<OrderCoupon>()
                .HasOne(oc => oc.Coupon)
                .WithMany(c => c.OrderCoupons)
                .HasForeignKey(oc => oc.CouponId);

            modelBuilder.Entity<Specification>()
                .HasOne(s => s.Product)
                .WithOne(p => p.Specification)
                .HasForeignKey<Specification>(s => s.ProductId);

            modelBuilder.Entity<WishListItem>()
                .HasKey(wli => new { wli.WishListId, wli.ProductId });

            modelBuilder.Entity<WishListItem>()
                .HasOne(wli => wli.WishList)
                .WithMany(w => w.WishListItems)
                .HasForeignKey(wli => wli.WishListId);

            modelBuilder.Entity<WishListItem>()
                .HasOne(wli => wli.Product)
                .WithMany(p => p.WishListItems)
                .HasForeignKey(wli => wli.ProductId);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        property.SetColumnType("nvarchar(max)");
                        property.SetCollation("Vietnamese_CI_AS");
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<phonezone_backend.Models.User> Users { get; set; } = default!;
        public DbSet<phonezone_backend.Models.Product> Products { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default;
        public DbSet<Cart> Carts { get; set; } = default;
        public DbSet<CartItem> CartItems { get; set; } = default;
        public DbSet<Coupon> Coupons { get; set; } = default;
        public DbSet<Order> Orders { get; set; } = default;
        public DbSet<OrderCoupon> OrdersCoupons { get; set; } = default;
        public DbSet<OrderDetail> OrderDetails { get; set; } = default;
        public DbSet<Specification> Specifications { get; set; } = default;
        public DbSet<WishList> WishList { get; set; } = default;
        public DbSet<WishListItem> WishListItems { get; set;} = default;

    }
}
