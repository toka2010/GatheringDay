namespace GatheringDay.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GatheringContext : DbContext
    {
        
        public GatheringContext()
            : base("name=GatheringContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ContactUs> Contacts { get; set; }
        //public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }

   
}