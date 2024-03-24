﻿using Domain.Travel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Travel.Context
{
    public class TravelContext : DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<HousingDescriptions> HousingDescriptions { get; set; }
        public DbSet<HousingFeatures> HousingFeatures { get; set; }
        public DbSet<HousingReview> HousingReviews { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerFeatures> OwnerFeatures { get; set; }
        public DbSet<OwnerReview> OwnerReviews { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<User>()
       .HasMany(u => u.OwnerReviews)
       .WithOne()
       .HasForeignKey(o => o.UserId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OwnerReview>()
       .HasOne(o => o.User)
       .WithMany()
       .HasForeignKey(o => o.UserId)
       .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Favorites>()
      .HasOne(f => f.Users)
      .WithMany(u => u.Favorites)
      .HasForeignKey(f => f.UserId)
      .IsRequired(false);

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.FavoriteHousings)
                .WithMany()
                .HasForeignKey(f => f.HousingId)
                .IsRequired(false);


            modelBuilder.Entity<HousingReview>()
.HasOne(f => f.User)
.WithMany(u => u.HousingReviews)
.HasForeignKey(f => f.UserId)
.IsRequired(false);

            modelBuilder.Entity<HousingReview>()
                .HasOne(f => f.Housing)
                .WithMany()
                .HasForeignKey(f => f.HousingId)
                .IsRequired(false);


            modelBuilder.Entity<Reservation>()
.HasOne(f => f.User)
.WithMany(u => u.Reservations)
.HasForeignKey(f => f.UserId)
.IsRequired(false);

            modelBuilder.Entity<Reservation>()
                .HasOne(f => f.Housing)
                .WithMany()
                .HasForeignKey(f => f.HousingId)
                .IsRequired(false);
        }
    }
}