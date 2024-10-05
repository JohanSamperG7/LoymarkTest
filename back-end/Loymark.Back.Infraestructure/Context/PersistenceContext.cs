using Loymark.Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Loymark.Back.Infraestructure.Context
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                return;

            modelBuilder.HasDefaultSchema(_configuration.GetSection("SchemaName").Value);
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(user => user.Id);
                user.Property(user => user.Id).ValueGeneratedOnAdd();
                user.HasMany(user => user.Activities)
                    .WithOne(activity => activity.User);
            });

            modelBuilder.Entity<Activity>(activity =>
            {
                activity.HasKey(activity => activity.Id);
                activity.Property(activity => activity.Id).ValueGeneratedOnAdd();
                activity.HasOne(activity => activity.User)
                    .WithMany(user => user.Activities);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
