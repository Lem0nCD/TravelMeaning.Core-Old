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
            modelBuilder.Entity<BindingTypeUser>().HasOne(x => x.User).WithMany(x => x.Bindings).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ChatMessage>().HasOne(x => x.Conversation).WithMany(x => x.ChatMessages).HasForeignKey(x => x.ConversationId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(x => x.TravelGuide).WithMany(x => x.Comments).HasForeignKey(x => x.TravelGuideId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CommentReview>().HasOne(x => x.Comment).WithOne(x => x.CommentReview).HasForeignKey<CommentReview>(x => x.CommentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Conversation>().HasOne(x => x.User1).WithMany(x => x.ToConversations).HasForeignKey(x => x.User1Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Conversation>().HasOne(x => x.User2).WithMany(x => x.FromConversations).HasForeignKey(x => x.User2Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favorite>().HasOne(x => x.User).WithMany(x => x.Favorites).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>().HasOne(x => x.ToUser).WithMany(x => x.ToUserMessages).HasForeignKey(x => x.ToUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>().HasOne(x => x.FromUser).WithMany(x => x.FromUserMessages).HasForeignKey(x => x.FromUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RelationShip>().HasOne(x => x.FromUser).WithMany(x => x.FromRelationShips).HasForeignKey(x => x.FromUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RelationShip>().HasOne(x => x.ToUser).WithMany(x => x.ToRelationShips).HasForeignKey(x => x.ToUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TravelGuide>().HasOne(x => x.User).WithMany(x => x.TravelGuides).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TravelGuideReview>().HasOne(x => x.TravelGuide).WithOne(x => x.TravelGuideReview).HasForeignKey<TravelGuideReview>(x => x.TravelGuideId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithOne(x => x.UserRole).HasForeignKey<UserRole>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<BindingTypeUser> BindingTypeUsers  { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReview> CommentReviews { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RelationShip> RelationShips { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TravelGuide> TravelGuides { get; set; }
        public DbSet<TravelGuideReview> TravelGuideReviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
