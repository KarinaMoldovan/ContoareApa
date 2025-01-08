using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ProiectContoareContext : DbContext
    {
        public ProiectContoareContext (DbContextOptions<ProiectContoareContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.Models.Factura> Factura { get; set; } = default!;
        public DbSet<WebAPI.Models.Tarif> Tarif { get; set; } = default!;
        public DbSet<WebAPI.Models.Consumator> Consumator { get; set; } = default!;
        public DbSet<WebAPI.Models.Contor> Contor { get; set; } = default!;
    }
}
