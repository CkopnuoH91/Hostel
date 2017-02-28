using System.Data.Entity;

namespace Hostel.Models
{
    public class HostelContext : DbContext
    {
        public HostelContext()
            : base("HostelDb")
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Person> Persons { get; set; }

    }
}
