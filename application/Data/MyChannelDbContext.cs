﻿using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Data
{
    public class MyChannelDbContext : DbContext
    {
        public MyChannelDbContext() { }
        public MyChannelDbContext(DbContextOptions<MyChannelDbContext> contextOptions) : base(contextOptions) { }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=MyChannelDbContext;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Comment
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>()
                .Property(x => x.UserId)
                .IsRequired();
            modelBuilder.Entity<Comment>()
                .Property(x => x.VideoId)
                .IsRequired();
            modelBuilder.Entity<Comment>()
                .Property(x => x.Content)
                .HasMaxLength(200)
                .IsRequired();
            modelBuilder.Entity<Comment>()
                .Property(x => x.Created)
                .IsRequired();
            #endregion
        }
    }
}
