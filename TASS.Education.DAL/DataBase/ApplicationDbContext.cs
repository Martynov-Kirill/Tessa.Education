using Microsoft.EntityFrameworkCore;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.DAL.DataBase
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Entity)))
                .ToList();

            var viewTypeAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var type in typesToRegister)
            {
                modelBuilder.Entity(type);
            }

            foreach (var assembly in viewTypeAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
