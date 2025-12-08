using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Lab2.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Lab2.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Lab2.Models.Ticket> Ticket { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ràng buộc mối quan hệ Customer <-> Ticket
            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Customer)        
            .WithMany(c => c.Tickets)       
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); 

        }
}
