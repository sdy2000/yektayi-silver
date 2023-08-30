using DataLayer.Entities.Permissions;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using DataLayer.Entities.Wallets;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class YektayiSilverContext : DbContext
    {
        public YektayiSilverContext(DbContextOptions<YektayiSilverContext> options)
            : base(options)
        {

        }

        #region USER

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserGender> UserGenders { get; set; }

        #endregion

        #region WALLET

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region PERMISSION

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region PRODUCT

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region USER

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            #endregion

            #region WALLET

            modelBuilder.Entity<WalletType>().HasData(
               new WalletType()
               {
                   TypeId = 1,
                   TypeTitle = "واریز"
               },
               new WalletType()
               {
                   TypeId = 2,
                   TypeTitle = "برداشت"
               });

            #endregion

            #region ROLE

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            #endregion

            #region PRODUCT

            modelBuilder.Entity<ProductStatus>()
                .HasData(
                new ProductStatus()
                {
                    StatusId=1,
                    StatusTitel = "موجود"
                },
                new ProductStatus()
                {
                    StatusId=2,
                    StatusTitel = "نا موجود"
                },
                new ProductStatus()
                {
                    StatusId=3,
                    StatusTitel = "به زودی"
                });



            modelBuilder.Entity<Product>()
             .HasOne<ProductGroup>(f => f.Group)
             .WithMany(g => g.Groups)
             .HasForeignKey(f => f.GroupId)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Product>()
               .HasOne<ProductGroup>(f => f.SubGroup)
               .WithMany(g => g.SubGroups)
               .HasForeignKey(f => f.SubGroupId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDelete);

            modelBuilder.Entity<ProductGroup>()
                .HasQueryFilter(p => !p.IsDelete);

            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
