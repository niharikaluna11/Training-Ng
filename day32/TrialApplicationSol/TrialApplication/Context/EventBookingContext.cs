using Microsoft.EntityFrameworkCore;
using TrialApplication.Models;

namespace TrialApplication.Context
{
    public class EventBookingContext : DbContext
    {

        public EventBookingContext(DbContextOptions contextOptions) : base(contextOptions)
        { }

        //tables are created
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        // m to m employee to event
        // employee to attendance
        // attendance to booking


        // 1 to m employee to booking 

        // 1 to 1 event to booking 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //booking primary key
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            //booking to employee 1 to many
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Booking)
                .HasForeignKey(b => b.EmployeeId)
                .HasConstraintName("FK_Booking_Employee");

            //booking to event 1 to many
            modelBuilder.Entity<Booking>()
               .HasOne(b => b.Event)
               .WithMany(e => e.Bookings)
               .HasForeignKey( b => b.EventId)
               .HasConstraintName("FK_Booking_Event");

            //attendance
            modelBuilder.Entity<Attendance>()
              .HasKey(a => a.AttendanceId);

        //attendance to employee
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .HasConstraintName("FK_Attendance_Employee");

            //attendance to entity
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Event)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EventId)
                .HasConstraintName("FK_Attendance_Event");

        }


}
}
