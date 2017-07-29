using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Quiron.LojaVirtual.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace Quiron.LojaVirtual.Dominio.Repositorio
{
    public class EfDbContext : DbContext    
    {
        /*DbSet Representa uma coleção de entidades no contexto.
          Serve para mapear a classe*/
          public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuider)
        {
            modelBuider.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuider.Entity<Produto>().ToTable("Produtos");
        }
    }
}
