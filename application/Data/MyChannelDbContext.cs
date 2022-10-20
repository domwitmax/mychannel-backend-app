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
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoLiked> VideoLikeds { get; set; }
        public DbSet<VideoStatus> VideoStatuses { get; set; }
        public DbSet<View> Views { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=MyChannelDbContext;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Account
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Created)
                .IsRequired();
            #endregion
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
            #region Setting
            modelBuilder.Entity<Setting>().ToTable("Setting");
            modelBuilder.Entity<Setting>().HasKey(x => x.UserId);
            modelBuilder.Entity<Setting>()
                .Property(x => x.UserId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Setting>()
                .Property(x => x.DarkMode)
                .IsRequired();
            #endregion
            #region Subscription
            modelBuilder.Entity<Subscription>().ToTable("Subscription");
            modelBuilder.Entity<Subscription>().HasKey(x => x.Id);
            modelBuilder.Entity<Subscription>()
                .Property(x => x.UserName)
                .IsRequired();
            modelBuilder.Entity<Subscription>()
                .Property(x => x.SubscriptionUserName)
                .IsRequired();
            #endregion
            #region Video
            modelBuilder.Entity<Video>().ToTable("Video");
            modelBuilder.Entity<Video>().HasKey(x => x.VideoId);
            modelBuilder.Entity<Video>()
                .Property(x => x.AuthorId)
                .IsRequired();
            modelBuilder.Entity<Video>()
                .Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();
            modelBuilder.Entity<Video>()
                .Property(x => x.Created)
                .IsRequired();
            #endregion
            #region VideoLiked
            modelBuilder.Entity<VideoLiked>().ToTable("VideoLiked");
            modelBuilder.Entity<VideoLiked>().HasKey(x => x.Id);
            modelBuilder.Entity<VideoLiked>()
                .Property(x => x.UserId)
                .IsRequired();
            modelBuilder.Entity<VideoLiked>()
                .Property(x => x.VideoId)
                .IsRequired();
            #endregion
            #region VideoStatus
            modelBuilder.Entity<VideoStatus>().ToTable("VideoStatus");
            modelBuilder.Entity<VideoStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<VideoStatus>()
                .Property(x => x.UserId)
                .IsRequired();
            modelBuilder.Entity<VideoStatus>()
                .Property(x => x.VideoId)
                .IsRequired();
            modelBuilder.Entity<VideoStatus>()
                .Property(x => x.VideoTime)
                .IsRequired();
            #endregion
            #region View
            modelBuilder.Entity<View>().ToTable("View");
            modelBuilder.Entity<View>().HasKey(x => x.Id);
            modelBuilder.Entity<View>()
                .Property(x => x.VideoId)
                .IsRequired();
            modelBuilder.Entity<View>()
                .Property(x => x.ViewDate)
                .IsRequired();
            #endregion
        }
    }
}
