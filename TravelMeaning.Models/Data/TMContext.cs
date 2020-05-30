using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Models.Data
{
    public class TMContext : DbContext
    {
        public TMContext(DbContextOptions<TMContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BindingTypeUser>(x =>
            { 
                x.HasOne(x => x.User).WithMany(x => x.Bindings).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Type).IsRequired();
            });

            modelBuilder.Entity<ChatMessage>(x =>
            {
                x.HasOne(x => x.Conversation).WithMany(x => x.ChatMessages).HasForeignKey(x => x.ConversationId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Content).HasColumnType("nvarchar(100)").IsRequired();
                x.Property(x => x.Type).IsRequired();
                x.Property(x => x.IsRead).IsRequired();
            });

            modelBuilder.Entity<Comment>(x =>
            {
                x.HasOne(x => x.TravelGuide).WithMany(x => x.Comments).HasForeignKey(x => x.TravelGuideId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Content).HasColumnType("nvarchar(1000)");
            });

            modelBuilder.Entity<CommentReview>(x =>
            {
                x.HasOne(x => x.Comment).WithOne(x => x.CommentReview).HasForeignKey<CommentReview>(x => x.CommentId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Note).HasColumnType("nvarchar(100)");
                x.Property(x => x.State).IsRequired();
            });

            modelBuilder.Entity<Conversation>(x =>
            {
                x.HasOne(x => x.User1).WithMany(x => x.ToConversations).HasForeignKey(x => x.User1Id).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.User2).WithMany(x => x.FromConversations).HasForeignKey(x => x.User2Id).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Favorite>(x =>
            {
                x.HasOne(x => x.User).WithMany(x => x.Favorites).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GuideRoute>(x =>
            {
                x.HasOne(x => x.TravelGuide).WithOne(x => x.GuideRoute).HasForeignKey<GuideRoute>(x => x.TravelGuideId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Message>(x =>
            {
                x.HasOne(x => x.ToUser).WithMany(x => x.ToUserMessages).HasForeignKey(x => x.ToUserId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.FromUser).WithMany(x => x.FromUserMessages).HasForeignKey(x => x.FromUserId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Content).HasColumnType("nvarchar(1000)").IsRequired();
                x.Property(x => x.Type).IsRequired();
            });

            modelBuilder.Entity<RelationShip>(x =>
            {
                x.HasOne(x => x.FromUser).WithMany(x => x.FromRelationShips).HasForeignKey(x => x.FromUserId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.ToUser).WithMany(x => x.ToRelationShips).HasForeignKey(x => x.ToUserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Role>(x =>
            {
                x.Property(x => x.Name).HasColumnType("nvarchar(20)");
                x.Property(x => x.Description).HasColumnType("nvarchar(200)");

            });
            modelBuilder.Entity<TravelGuide>(x =>
            {
                x.HasOne(x => x.User).WithMany(x => x.TravelGuides).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Title).HasColumnType("nvarchar(100)").IsRequired();
                x.Property(x => x.Content).HasColumnType("nvarchar(max)").IsRequired();
                x.Property(x => x.CoverImage).HasColumnType("nvarchar(1000)");
            });

            modelBuilder.Entity<TravelGuideReview>(x =>
            {
                x.HasOne(x => x.TravelGuide).WithOne(x => x.TravelGuideReview).HasForeignKey<TravelGuideReview>(x => x.TravelGuideId).OnDelete(DeleteBehavior.Restrict);
                x.Property(x => x.Note).HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<User>(x =>
            {
                x.Property(x => x.Username).HasColumnType("nvarchar(10)").IsRequired();
                x.Property(x => x.Password).HasColumnType("nvarchar(20)").IsRequired();
                x.Property(x => x.Avatar).HasColumnType("nvarchar(100)");
                x.Property(x => x.Location).HasColumnType("nvarchar(100)");
                x.Property(x => x.Occupation).HasColumnType("nvarchar(100)");
                x.Property(x => x.PhoneNumber).HasColumnType("nvarchar(100)").IsRequired();
            });

            modelBuilder.Entity<UserRole>(x =>
            {
                x.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            });

        }
        public DbSet<BindingTypeUser> BindingTypeUsers  { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReview> CommentReviews { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<GuideRoute> GuideRoutes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RelationShip> RelationShips { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TravelGuide> TravelGuides { get; set; }
        public DbSet<TravelGuideReview> TravelGuideReviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
