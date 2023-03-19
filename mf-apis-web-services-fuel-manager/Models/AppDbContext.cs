using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Configuração keys/relacionamento tabela VeiculoUsuarios
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // PK Composta
            builder.Entity<VeiculoUsuarios>()
                .HasKey(c => new { c.VeiculoId, c.UsuarioId });

            // Relacionamento 1 Veic : N User
            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Veiculo)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.VeiculoId);

            // Relacionamento 1 User : N Veic
            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Usuario)
                .WithMany(c => c.Veiculos)
                .HasForeignKey(c => c.UsuarioId);
        }

        /* Entidades */
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VeiculoUsuarios> VeiculoUsuarios { get; set; }
    }
}

// Anotações ----------------------------------------
/*
 Injeção de dependência: É para não ficar criando instâncias de forma programática e sim para criar dependência entre os objetos. Ela faz isso via configuração, o programa vai injetar/carregar a classe automaticamente.
    Se formos usar intâncias, teríamos que declar new... e  configurar ela.
 */



/* Para não precisar de criar instâncias da classe de forma programática, ou seja, criando classes e configurando manualmente.
  Com a injeção de dependência, apenas configuramos a classe e ela faz isso sem precisar de programar manual
 */


/*
 PK compostas
 .HasKey(c => new { c.VeiculoId, c.UsuarioId });        
 */