using Microsoft.EntityFrameworkCore;
using PersonsManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Database
{
    public class PersonsManagerDbContext : DbContext
    {
        public PersonsManagerDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
