using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysMaTra.Models;

public class SysMaTraContext : IdentityDbContext<IdentityUser>
{
        public SysMaTraContext (DbContextOptions<SysMaTraContext> options)
            : base(options)
        {
        }

        public DbSet<SysMaTra.Models.Bagage> Bagage { get; set; } = default!;

        public DbSet<SysMaTra.Models.Adresse> Adresse { get; set; }

        public DbSet<SysMaTra.Models.Bus> Bus { get; set; }

        public DbSet<SysMaTra.Models.Colis> Colis { get; set; }

        public DbSet<SysMaTra.Models.Agence> Agence { get; set; }

        public DbSet<SysMaTra.Models.Configuration> Configuration { get; set; }

        public DbSet<SysMaTra.Models.Couleur> Couleur { get; set; }

        public DbSet<SysMaTra.Models.Destination> Destination { get; set; }

        public DbSet<SysMaTra.Models.Passager> Passager { get; set; }

        public DbSet<SysMaTra.Models.Personnel> Personnel { get; set; }

        public DbSet<SysMaTra.Models.Requete> Requete { get; set; }

        public DbSet<SysMaTra.Models.Trajet> Trajet { get; set; }

        public DbSet<SysMaTra.Models.Reservation> Reservation { get; set; }

        public DbSet<SysMaTra.Models.Voyage> Voyage { get; set; }
    }
