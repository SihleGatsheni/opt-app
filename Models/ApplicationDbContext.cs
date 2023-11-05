using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OptiApp.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Slot>().HasNoKey();
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Optometrist> Optometrists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Slot> Slots { get; set; } 
        public virtual DbSet<Receptionist> Receptionists { get; set; }

    }
}
