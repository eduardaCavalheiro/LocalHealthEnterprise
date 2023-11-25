using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalHealth1.Models;

namespace LocalHealth1.Data
{
    public class LocalHealth1Context : DbContext
    {
        public LocalHealth1Context (DbContextOptions<LocalHealth1Context> options)
            : base(options)
        {
        }

        public DbSet<LocalHealth1.Models.Medico> Medico { get; set; } = default!;

        public DbSet<LocalHealth1.Models.Localizacao> Localizacao { get; set; } = default!;

        public DbSet<LocalHealth1.Models.Doenca> Doenca { get; set; } = default!;

        public DbSet<LocalHealth1.Models.Diagnostico> Diagnostico { get; set; } = default!;

        public DbSet<LocalHealth1.Models.DetalheDiagnostico> DetalheDiagnostico { get; set; } = default!;
    }
}
