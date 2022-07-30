﻿using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Real.Time.Chat.Infrastructure.Data.Context
{
    public class RealTimeChatContext : DbContext
    {
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public RealTimeChatContext(DbContextOptions<RealTimeChatContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                 .AddJsonFile($"appsettings.Development.json")
#else
                 .AddJsonFile($"appsettings.Production.json")
#endif
                 .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("RealTimeChatConnection"));
        }
    }
}
