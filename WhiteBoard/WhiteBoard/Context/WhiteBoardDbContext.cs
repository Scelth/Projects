using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WhiteBoard.Model;

namespace WhiteBoard.Context
{
    public class WhiteBoardDbContext : DbContext
    {
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<PicturesModel> Pictures { get; set; }
        public DbSet<KeepModel> Keep { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("LocalConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<UsersModel>();
            var pictures = modelBuilder.Entity<PicturesModel>();
            var keepers = modelBuilder.Entity<KeepModel>();

            // primary key 
            users.HasKey(x => x.ID);
            pictures.HasKey(x => x.ID);
            keepers.HasKey(x => x.ID);

            // Not null
            users.Property(x => x.Username).IsRequired();
            users.Property(x => x.Password).IsRequired();

            pictures.Property(x => x.Name).IsRequired();
            pictures.Property(x => x.Date).IsRequired();
            pictures.Property(x => x.PicturePath).IsRequired();
            pictures.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserID);

            keepers.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserID);
        }
    }
}
