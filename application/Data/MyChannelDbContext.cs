using Application.Data.Entities;
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
        public MyChannelDbContext(DbContextOptions<MyChannelDbContext> contextOptions) : base(contextOptions) { }
        public DbSet<Comment> Comments;

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
