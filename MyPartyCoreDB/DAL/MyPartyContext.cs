using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.DB.DAL
{
    public class MyPartyContext : IdentityDbContext<User>
    {
        public MyPartyContext(DbContextOptions<MyPartyContext> options)
            : base(options)
        {}

        public DbSet<Party> Parties { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<FileModel> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PartyConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantConfiguration());

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("admin") { Id = "ccfabe84-124f-473b-81a0-5da1d8ab4857", ConcurrencyStamp = "81b2fd77-9615-4927-98f6-0c8dce30a290", NormalizedName = "ADMIN" },
                new IdentityRole("user") { Id = "53dda6a0-e534-4fac-b3d2-145a7c3e2752", ConcurrencyStamp = "8eb0db2d-383a-4d73-96a2-c1e29cf58af5", NormalizedName = "USER" });
        }
    }

    public class FileConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Path).IsRequired().HasMaxLength(256);
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Sex).IsRequired();
            builder.Property(p => p.Birthday).IsRequired();
            builder.HasOne(p => p.Avatar).WithOne().HasForeignKey<User>(p => p.AvatarId);
        }
    }

    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(256);
            builder.Property(p => p.AgeLimit).IsRequired().HasDefaultValue(false);
            builder.HasOne(p => p.Owner)
                .WithMany(t => t.Parties)
                .HasForeignKey(p => p.OwnerId);

        }
    }

    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Attend).IsRequired();
            builder.Property(p => p.Reason).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(256);
            builder.HasOne(p => p.User)
                .WithMany(t => t.Participants)
                .HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Party)
                .WithMany(t => t.Participants)
                .HasForeignKey(p => p.PartyId);
        }
    }
}
