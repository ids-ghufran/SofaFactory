using System.Runtime.Intrinsics.Arm;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SofaFactory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Domain.Models.Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<SeatingCapacity> SeatingCapacities { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }
        public DbSet<Slider> Slider { get; set; }


        //roles seeding

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
        }

        private readonly string RAdmin = "d6c200c6-ff1f-4bd8-a1a3-92dc234d254b";
        private readonly string RSubAdmin = "df129349-c335-46e1-86e6-22a179d808d3";
        private readonly string RUser = "8883fbd9-4420-4444-84ca-ca31e8221cc7";
        private readonly string UAdmin = "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa";
        private readonly string USubAdmin = "3f67700c-232a-4bc1-b3b5-f8aef4630b1e";



        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
              new IdentityRole() { Id = RAdmin, Name = "Admin", ConcurrencyStamp = "92dc234d254b", NormalizedName = "ADMIN" },
              new IdentityRole() { Id = RSubAdmin, Name = "SubAdmin", ConcurrencyStamp = "22a179d808d3", NormalizedName = "SUBADMIN" },
              new IdentityRole() { Id = RUser, Name = "User", ConcurrencyStamp = "ca31e8221cc7", NormalizedName = "USER" }

  );
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();

            var appUserList = new List<AppUser>()
            {
                new AppUser
            {
                Id = UAdmin,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = ph.HashPassword(null, "Ids@1234")
            },

                new AppUser
            {
                Id = USubAdmin,
                UserName = "subadmin",
                NormalizedUserName = "SUBADMIN",
                PasswordHash = ph.HashPassword(null, "Ids@1234")
            }

        };

            //seed user
            builder.Entity<AppUser>().HasData(appUserList);
        }


        private void SeedUserRoles(ModelBuilder builder)
        {
            var userRoles = new List<IdentityUserRole<string>>()
            {
                //super user roles
                new IdentityUserRole<string>() { RoleId = RAdmin, UserId = UAdmin},
                new IdentityUserRole<string>() { RoleId = RSubAdmin, UserId = USubAdmin }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }

    }
}