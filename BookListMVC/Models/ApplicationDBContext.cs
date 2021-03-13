using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    // migrate configuration
    public class ApplicationDBContext : DbContext
    {
        // pass options to base class
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        // add Books table
        public DbSet<Book> Books { get; set; }
    }
}
