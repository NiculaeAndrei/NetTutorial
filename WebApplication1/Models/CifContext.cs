using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Models
{
    public class CifContext:DbContext
    {
        public CifContext(DbContextOptions<CifContext> options)
          : base(options)
        {

        }
        public DbSet<Cif> Cifs { get; set; }
    }
}
