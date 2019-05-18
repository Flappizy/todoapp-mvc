using Microsoft.EntityFrameworkCore;
using System;
using TodoMVC.Data.Models;

namespace TodoMVC.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TodoListDB;Trusted_Connection=True;");
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("isCompleted")
                    .IsRequired();
            });
        }
    }
}
