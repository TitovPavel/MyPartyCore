using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPartyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.DAL
{
    public class MyPartyContext : IdentityDbContext<User>
    {
        public MyPartyContext(DbContextOptions<MyPartyContext> options)
            : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhoneConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class PhoneConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(255);
            builder.Ignore(p => p.User);
            builder.Ignore(p => p.UserId);

        }
    }
    public class CompanyConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Attend).IsRequired();
            builder.Property(p => p.Reason).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(255);
            builder.Ignore(p => p.User);
            builder.Ignore(p => p.UserId);
            builder.HasOne(p => p.Party)
            .WithMany(t => t.Participants)
            .HasForeignKey(p => p.PartyId);
        }
    }
}
