using Microsoft.EntityFrameworkCore;
using Web_Api_Assignment_049_.Models;

namespace Web_Api_Assignment_049_.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
