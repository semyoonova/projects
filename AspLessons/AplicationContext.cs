using AspLessons.Helpers;
using Microsoft.EntityFrameworkCore;
namespace AspLessons
{
    

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Favor> Favors => Set<Favor>();
        public DbSet<Master> Masters => Set<Master>();
        public DbSet<WorkHours> WorkHours => Set<WorkHours>();
        public DbSet<Register> Registers => Set<Register>();
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>( )
                .Property(b => b.PhoneNumber).HasMaxLength(13);
            modelBuilder.Entity<User>( )
                .Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<User>( )
                .HasIndex(p => new { p.Name, p.PhoneNumber, p.Password })
                .IsUnique( );

            modelBuilder.Entity<Favor>( )
                .HasIndex(p => p.FavorName)
                .IsUnique( );
            modelBuilder.Entity<Favor>( )
                .Property(b => b.FavorName).HasMaxLength(50);

            modelBuilder.Entity<Master>( )
                .HasIndex(p => p.Name)
                .IsUnique( );
            modelBuilder.Entity<Master>( )
                .Property(b => b.Name).HasMaxLength(50);

            
            foreach(var entityType in modelBuilder.Model.GetEntityTypes( ))
            {
                foreach(var property in entityType.GetProperties( ))
                {
                    if(property.ClrType == typeof(DateTime))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }

        }

    }
}
