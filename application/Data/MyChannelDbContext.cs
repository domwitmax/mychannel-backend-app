using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public class MyChannelDbContext : DbContext
    {
        public MyChannelDbContext(DbContextOptions<MyChannelDbContext> contextOptions) : base(contextOptions) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoLiked> VideoLikeds { get; set; }
        public DbSet<View> Views { get; set; }

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
