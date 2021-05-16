using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLibrary
{
    public class EFDBContext : DbContext
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Readers> Readers { get; set; }
        public DbSet<ManagementBooks> ManagementBooks { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>(); 
            optionsBuilder.UseSqlServer("Server=DESKTOP-O489HSH;DataBase=DBLibrary;Trusted_Connection=true;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataLibrary"));
            return new EFDBContext(optionsBuilder.Options);
        }
    }
}
