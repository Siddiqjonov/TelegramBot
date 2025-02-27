using Microsoft.EntityFrameworkCore;
using RamadonTaqvimBot.Dal.BotUserEntity;
using RamadonTaqvimBot.Dal.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamadonTaqvimBot.Dal;

public class MainContext : DbContext
{
    public DbSet<BotUser> BotUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured is false)
        {
            var connectionString = "Data Source=localhost\\SQLEXPRESS;User ID=sa;Password=1;Initial Catalog=TaqvimBot;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BotUserConfiguration());
    }
}
